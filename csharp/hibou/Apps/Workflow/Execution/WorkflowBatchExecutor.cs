using System;
using System.Collections.Generic;
using System.Threading;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Executes a batch of workflows.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    internal class WorkflowBatchExecutor : 
        IDisposable
    {
        #region Ctor

        public WorkflowBatchExecutor(WorkflowBatch batch)
        {
            // Defensive programming.
            if (batch == null)
                throw new ArgumentNullException("batch");
            if (batch.Count == 0)
                throw new ArgumentException("Workflow batch is empty and therefore cannot be executed.");
            if (batch.IsComplete)
                throw new ArgumentException("Workflow batch has already been executed.");
            Batch = batch;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether the workflow runtime events have been hooked to.
        /// </summary>
        private bool RuntimeEventsHooked
        { get; set; }

        /// <summary>
        /// Gets or sets the batch currently being executed.
        /// </summary>
        private WorkflowBatch Batch
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executes the batch of workflows.
        /// </summary>
        /// <param name="batch">The batch of workflows to be executed.</param>
        internal void ExecuteBatch()
        {
            // Process items within the batch.
            foreach (KeyValuePair<Guid, WorkflowInstance> kvp in Batch)
            {
                // Hook to event handlers.
                if (!RuntimeEventsHooked)
                    HookToRuntimeEventHandlers(true);

                // Execute workflow.
                Execute(kvp.Value);

                // Block calling thread until the item has completed (if necessary).
                Batch.BlockItemThread(kvp.Value);
            }

            // Block the calling thread until the batch has completed (if necessary).
            Batch.BlockThread();
        }

        /// <summary>
        /// Executes the passed workflow.
        /// </summary>
        /// <param name="workflow">The workflow instance being executed.</param>
        private void Execute(
            WorkflowInstance workflow)
        {
            // Start the workflow.
            workflow.Start();

            // When manually scheduling we must run the workflow ourselves.
            ManualWorkflowSchedulerService manualScheduler =
                workflow.WorkflowRuntime.GetService<ManualWorkflowSchedulerService>();
            if (manualScheduler != null)
                manualScheduler.RunWorkflow(workflow.InstanceId);
        }

        /// <summary>
        /// Hooks upto workflow runtime event handlers.
        /// </summary>
        /// <param name="runtime">The runtime to hook to.</param>
        /// <param name="hook">Flag to indicate whether to hook or unhook.</param>
        private void HookToRuntimeEventHandlers(
            bool hook)
        {
            // Exit if the conditions are not right.
            Dictionary<string, WorkflowRuntime> runtimeDictionary = Batch.GetRuntimeDictionary();
            if (runtimeDictionary.Count == 0)
                return;
            
            // Either hook or unhook to event handlers.
            foreach (KeyValuePair<string, WorkflowRuntime> kvp in runtimeDictionary)
            {
                if (hook)
                {
                    kvp.Value.WorkflowAborted += OnWorkflowEvent_Aborted;
                    kvp.Value.WorkflowStarted += OnWorkflowEvent_Started;
                    kvp.Value.WorkflowCompleted += OnWorkflowEvent_Completed;
                    kvp.Value.WorkflowIdled += OnWorkflowEvent_Idled;
                    kvp.Value.WorkflowTerminated += OnWorkflowEvent_Terminated;
                }
                else
                {
                    kvp.Value.WorkflowAborted -= OnWorkflowEvent_Aborted;
                    kvp.Value.WorkflowStarted -= OnWorkflowEvent_Started;
                    kvp.Value.WorkflowCompleted -= OnWorkflowEvent_Completed;
                    kvp.Value.WorkflowIdled -= OnWorkflowEvent_Idled;
                    kvp.Value.WorkflowTerminated -= OnWorkflowEvent_Terminated;
                }
            }

            // Cache the flag.
            RuntimeEventsHooked = hook;
        }

        #endregion Methods

        #region Workflow runtime event handlers

        /// <summary>
        /// Handles the workflow idled event.
        /// </summary>
        private void OnWorkflowEvent_Idled(object sender, WorkflowEventArgs args)
        {
            // Exit immediately if the conditions are not right.
            if (!Batch.ContainsWorkflow(args.WorkflowInstance))
                return;

            // TODO add autologging.

            // Unblock the thread if there is a matching wait handle.
            UnblockWorkflowThread(args.WorkflowInstance);

            // If running synchronously then keep the workflow spinning.
            if (args.WorkflowInstance.WorkflowRuntime.GetService<ManualWorkflowSchedulerService>() != null)
            {
                // Set a system timer to reload this workflow when its next timer expires
                SetReloadWorkflowTimer(args.WorkflowInstance);
            }
            // Otherwise Flag that the event has completed.
            else
            {
                readyHandle.Set();
            }
        }

        /// <summary>
        /// Handles the workflow completed event.
        /// </summary>
        private void OnWorkflowEvent_Completed(object sender, WorkflowCompletedEventArgs args)
        {
            // Exit immediately if the conditions are not right.
            if (!Batch.ContainsWorkflow(args.WorkflowInstance))
                return;

            // TODO add autologging.

            // Unblock the thread if there is a matching wait handle.
            UnblockWorkflowThread(args.WorkflowInstance);
        }

        /// <summary>
        /// Handles the workflow terminated event.
        /// </summary>
        private void OnWorkflowEvent_Terminated(object sender, WorkflowTerminatedEventArgs args)
        {
            // Exit immediately if the conditions are not right.
            if (!Batch.ContainsWorkflow(args.WorkflowInstance))
                return;

            // TODO add autologging.

            // Unblock the thread if there is a matching wait handle.
            UnblockWorkflowThread(args.WorkflowInstance);
        }

        /// <summary>
        /// Handles the workflow started event.
        /// </summary>
        private void OnWorkflowEvent_Started(object sender, WorkflowEventArgs args)
        {
            // Exit immediately if the conditions are not right.
            if (!Batch.ContainsWorkflow(args.WorkflowInstance))
                return;

            // TODO add autologging.
        }

        /// <summary>
        /// Handles the workflow aborted event.
        /// </summary>
        private void OnWorkflowEvent_Aborted(object sender, WorkflowEventArgs args)
        {
            // Exit immediately if the conditions are not right.
            if (!Batch.ContainsWorkflow(args.WorkflowInstance))
                return;

            // TODO add autologging.

            // Unblock the thread if there is a matching wait handle.
            UnblockWorkflowThread(args.WorkflowInstance);
        }

        #endregion Workflow runtime event handlers

        #region Threading related code

        /// <summary>
        /// Threading field to signal that an event has occurred.
        /// </summary>
        private readonly AutoResetEvent readyHandle = new AutoResetEvent(false);

        /// <summary>
        /// Reloads the workflow after it has idled - only fires when a persistence service is loaded into the WF runtime.
        /// </summary>
        /// <param name="state"></param>
        private void ReloadWorkflow(object state)
        {
            WorkflowInstance workflow = state as WorkflowInstance;
            if (state != null)
            {
                if (workflow.GetWorkflowNextTimerExpiration() > DateTime.UtcNow)
                {
                    SetReloadWorkflowTimer(workflow);
                }
                else
                {
                    readyHandle.Set();
                }
            }
        }

        /// <summary>
        /// Ensures that manually scheduled workflows are executed.
        /// </summary>
        /// <param name="workflow">The workflow being reloaded.</param>
        private void SetReloadWorkflowTimer(WorkflowInstance workflow)
        {
            DateTime reloadTime = workflow.GetWorkflowNextTimerExpiration();
            if (reloadTime == DateTime.MaxValue)
            {
                readyHandle.Set();
            }
            else
            {
                TimeSpan timeDifference =
                    reloadTime -
                    DateTime.UtcNow +
                    // account for the race in this code
                    // the timer is set later than the timespan was measured
                    new TimeSpan(0, 0, 0, 0, 1);

                Timer timer = new Timer(
                    new TimerCallback(ReloadWorkflow),
                    null,
                    timeDifference < TimeSpan.Zero ? TimeSpan.Zero : timeDifference,
                    new TimeSpan(-1));
            }
        }

        /// <summary>
        /// Invoked when a managed workflow current execution cycle has finished.
        /// </summary>
        /// <param name="workflow">A workflow whose current execution cycle has completed.</param>
        private void UnblockWorkflowThread(
            WorkflowInstance workflow)
        {
            // Report that a managed workflow has completed.
            Batch.OnWorkflowCompleted(workflow.InstanceId);

            // Unblock the batch item thread if neceesary.
            Batch.UnblockBatchItemThread();

            // If batch is complete then remove from work in progress 
            // and unblock the calling thread (if necessary).
            if (Batch.IsComplete)
            {
                Batch.UnblockBatchThread();
            }
        }

        #endregion Threading related code

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            HookToRuntimeEventHandlers(false);
        }

        #endregion
    }
}
