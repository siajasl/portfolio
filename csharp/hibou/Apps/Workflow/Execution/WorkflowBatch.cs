using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Represents a batch of workflows being executed.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public class WorkflowBatch : 
        Dictionary<Guid, WorkflowInstance>
    {
        #region Ctor

        public WorkflowBatch()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Initialises member variables.
        /// </summary>
        private void InitialiseMembers()
        {
            Id = Guid.NewGuid();
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the batch id.
        /// </summary>
        public Guid Id
        { get; private set; }

        /// <summary>
        /// Gets or sets the identifier of the host workflow within which the batch is running.
        /// </summary>
        public Guid HostWorkflowInstanceId
        { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the host workflow runtime within which the host workflow is running.
        /// </summary>
        public string HostWorkflowRuntimeId
        { get; set; }

        /// <summary>
        /// Gets or sets the batch execution type.
        /// </summary>
        public WorkflowBatchExecutionType ExecutionType
        { get; set; }

        /// <summary>
        /// Gets or sets the calling thread context type.
        /// </summary>
        public WorkflowThreadContextType ThreadContextType
        { get; set; }

        /// <summary>
        /// Gets or sets a wait handle used for calling thread blocking at the batch level.
        /// </summary>
        private AutoResetEvent WaitHandle
        { get; set; }

        /// <summary>
        /// Gets or sets a wait handle used for calling thread blocking at the batch level.
        /// </summary>
        private AutoResetEvent ItemWaitHandle
        { get; set; }

        /// <summary>
        /// Gets or sets the number of completed workflows.
        /// </summary>
        public int CompletedWorkflows
        { get; set; }

        /// <summary>
        /// Gets a flag indicating whether all the work within the batch is complete.
        /// </summary>
        public bool IsComplete
        {
            get { return (CompletedWorkflows == base.Count); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a workflow instance to the batch.
        /// </summary>
        /// <param name="workflow">The workflow instance being added to the batch.</param>
        internal void AddWorkflow(
            WorkflowInstance workflow)
        {
            if (workflow == null)
                throw new ArgumentNullException("workflow");
            Add(workflow.InstanceId, workflow);
        }

        /// <summary>
        /// Blocks the calling thread.
        /// </summary>
        /// <param name="workflow">The workflow item being managed by the batch.</param>
        public void BlockItemThread(WorkflowInstance workflow)
        {
            if ((ItemWaitHandle == null) &&
                (DoBlockItemThread(workflow)))
            {
                ItemWaitHandle = new AutoResetEvent(false);
                ItemWaitHandle.WaitOne();
            }
        }

        /// <summary>
        /// Returns a flag indicating whether the calling thread will be blocked for the batch item.
        /// </summary>
        /// <param name="workflow">The workflow instance being executed.</param>
        /// <returns>True if the calling thread will be blocked.</returns>
        private bool DoBlockItemThread(
            WorkflowInstance workflow)
        {
            bool result = false;
            if ((ExecutionType == WorkflowBatchExecutionType.Sequential) &&
                (Count > 1) &&
                (workflow.WorkflowRuntime.GetService<ManualWorkflowSchedulerService>() == null))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Blocks the calling thread.
        /// </summary>
        public void BlockThread()
        {
            if ((WaitHandle == null) &&
                (DoBlockThread()))
            {
                WaitHandle = new AutoResetEvent(false);
                WaitHandle.WaitOne();
            }
        }

        /// <summary>
        /// Returns a flag indicating whether the calling thread will be blocked for the batch.
        /// </summary>
        /// <returns>True if the calling thread will be blocked.</returns>
        private bool DoBlockThread()
        {
            bool result = false;
            WorkflowRuntime runtime =
                WorkflowRuntimeManager.GetRuntime(HostWorkflowRuntimeId);
            if ((runtime != null) &&
                (ThreadContextType == WorkflowThreadContextType.Synch) &&
                (runtime.GetService<ManualWorkflowSchedulerService>() == null))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Unblocks the calling thread.
        /// </summary>
        public void UnblockBatchItemThread()
        {
            if (ItemWaitHandle != null)
            {
                ItemWaitHandle.Set();
                ItemWaitHandle = null;
            }
        }

        /// <summary>
        /// Unblocks the calling thread.
        /// </summary>
        public void UnblockBatchThread()
        {
            if (WaitHandle != null)
            {
                WaitHandle.Set();
                WaitHandle = null;
            }
        }

        /// <summary>
        /// Determines whether the batch contains the passed workflow.
        /// </summary>
        /// <param name="workflow">The workflow that may or may not be in the batch.</param>
        /// <returns>True if found with the managed list.</returns>
        public bool ContainsWorkflow(WorkflowInstance workflow)
        {
            WorkflowInstance match;
            return TryGetValue(workflow.InstanceId, out match);
        }

        /// <summary>
        /// Called when a workflow within the batch completes.
        /// </summary>
        /// <param name="workflowInstanceId">The workflow instance id that has completed.</param>
        public void OnWorkflowCompleted(
            Guid workflowInstanceId)
        {
            if (ContainsKey(workflowInstanceId))
                CompletedWorkflows++;
        }

        /// <summary>
        /// Returns a dictionary of all the workflow runtimes being used across the workflow within the batch.
        /// </summary>
        /// <remarks>
        /// The dictionary key is the workflow runtime name.
        /// </remarks>
        /// <returns>A list of workflow runtimes.</returns>
        public Dictionary<string, WorkflowRuntime> GetRuntimeDictionary()
        {
            Dictionary<string, WorkflowRuntime> result = 
                new Dictionary<string, WorkflowRuntime>();
            foreach (KeyValuePair<Guid, WorkflowInstance> kvp in this)
            {
                WorkflowRuntime runtime;
                if (!result.TryGetValue(kvp.Value.WorkflowRuntime.Name, out runtime))
                {
                    result.Add(kvp.Value.WorkflowRuntime.Name, kvp.Value.WorkflowRuntime);
                }
            }
            return result;
        }

        
        #endregion Methods
    }
}