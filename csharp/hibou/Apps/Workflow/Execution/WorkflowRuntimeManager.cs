using System;
using System.Collections.Generic;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Factory pattern for returning instances of the workflow runtime configured according to the execution context.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    internal sealed class WorkflowRuntimeManager
    {
        #region Constructor

        private WorkflowRuntimeManager() { }

        #endregion Constructor

        #region Singleton instance

        #region Singleton instance for synchronous threading context

        /// <summary>
        /// Static instance of the synchronously threaded runtime.
        /// </summary>
        private static WorkflowRuntime synchronousInstanceField = null;

        /// <summary>
        /// Readonly lock for ensuring thread safety.
        /// </summary>
        private static readonly object synchronousPadlock = new object();

        /// <summary>
        /// The thread safe single instance of the synchronously threaded runtime.
        /// </summary>
        internal static WorkflowRuntime SynchronousInstance
        {
            get
            {
                lock (synchronousPadlock)
                {
                    return synchronousInstanceField;
                }
            }
            set
            {
                lock (synchronousPadlock)
                {
                    synchronousInstanceField = value;
                }
            }
        }

        #endregion Singleton instance for synchronous threading context

        #region Singleton instance for asynchronous threading context

        /// <summary>
        /// Static instance of the asynchronously threaded runtime.
        /// </summary>
        private static WorkflowRuntime asynchronousInstanceField = null;

        /// <summary>
        /// Readonly lock for ensuring thread safety.
        /// </summary>
        private static readonly object asynchronousPadlock = new object();

        /// <summary>
        /// The thread safe single instance of the asynchronously threaded runtime.
        /// </summary>
        internal static WorkflowRuntime AsynchronousInstance
        {
            get
            {
                lock (asynchronousPadlock)
                {
                    return asynchronousInstanceField;
                }
            }
            set
            {
                lock (asynchronousPadlock)
                {
                    asynchronousInstanceField = value;
                }
            }
        }

        #endregion Singleton instance for synchronous threading context

        #endregion Singleton instance

        #region Methods

        /// <summary>
        /// Returns an instance of one of the managed runtimes.
        /// </summary>
        /// <param name="workflowThreadContext">The workflow thread context type.</param>
        /// <returns>A workflow runtime instance.</returns>
        internal static WorkflowRuntime GetRuntime(
            WorkflowThreadContextType workflowThreadContext)
        {
            WorkflowRuntime result = null;

            // Ensure that I am initialised.
            Initialise(workflowThreadContext);
            
            // Return relevant instance.
            if (workflowThreadContext == WorkflowThreadContextType.Synch)
                result = SynchronousInstance;
            else if (workflowThreadContext == WorkflowThreadContextType.Asynch)
                result = AsynchronousInstance;

            return result;
        }

        /// <summary>
        /// Searches for a runtime instance by runtime name.
        /// </summary>
        /// <param name="searchCriteria">The runtime name to use for the search.</param>
        /// <returns>A workflow runtime instance.</returns>
        internal static WorkflowRuntime GetRuntime(
            string searchCriteria)
        {
            if (AsynchronousInstance != null &&
                AsynchronousInstance.Name.Equals(searchCriteria))
                return AsynchronousInstance;
            else if (SynchronousInstance != null &&
                     SynchronousInstance.Name.Equals(searchCriteria))
                return SynchronousInstance;
            else
                return null;
        }

        /// <summary>
        /// Initialises a workflow runtime.
        /// </summary>
        /// <param name="workflowThreadContext">The workflow thread context type.</param>
        private static void Initialise(
            WorkflowThreadContextType workflowThreadContext)
        {
            if (workflowThreadContext == WorkflowThreadContextType.Asynch &&
                AsynchronousInstance == null)
            {
                AsynchronousInstance = WorkflowRuntimeFactory.Create(WorkflowThreadContextType.Asynch);
                AsynchronousInstance.Started += OnWorkflowRuntimeStarted;
                AsynchronousInstance.Stopped += OnWorkflowRuntimeStarted;
                AsynchronousInstance.StartRuntime();
            }
            if (workflowThreadContext == WorkflowThreadContextType.Synch &&
                SynchronousInstance == null)
            {
                SynchronousInstance = WorkflowRuntimeFactory.Create(WorkflowThreadContextType.Synch);
                SynchronousInstance.Started += OnWorkflowRuntimeStarted;
                SynchronousInstance.Stopped += OnWorkflowRuntimeStarted;
                SynchronousInstance.StartRuntime();
            }
        }

        /// <summary>
        /// Event handler for workflow runtime start events.
        /// </summary>
        private static void OnWorkflowRuntimeStarted(object sender, WorkflowRuntimeEventArgs e)
        {
            WorkflowRuntime runtime = sender as WorkflowRuntime;
            if (runtime != null)
            {
                if ((AsynchronousInstance != null) &&
                    (runtime.Name.Equals(AsynchronousInstance.Name)))
                {
                    //Logger.Instance.Log(
                    //    LogMessageCategory.Operational,
                    //    LogEventType.Information,
                    //    0,
                    //    AppTier.Workflow,
                    //    System.Reflection.MethodBase.GetCurrentMethod(),
                    //    null,
                    //    @"Asynchronous workflow runtime started : " + runtime.Name);
                }
                else if ((SynchronousInstance != null) &&
                    (runtime.Name.Equals(SynchronousInstance.Name)))
                {
                    //Logger.Instance.Log(
                    //    LogMessageCategory.Operational,
                    //    LogEventType.Information,
                    //    0,
                    //    AppTier.Workflow,
                    //    System.Reflection.MethodBase.GetCurrentMethod(),
                    //    null,
                    //    @"Synchronous workflow runtime started : " + runtime.Name);
                }
            }
        }

        /// <summary>
        /// Event handler for workflow runtime stop events.
        /// </summary>
        private static void OnWorkflowRuntimeStopped(object sender, WorkflowRuntimeEventArgs e)
        {
            WorkflowRuntime runtime = sender as WorkflowRuntime;
            if (runtime != null)
            {
                if ((AsynchronousInstance != null) &
                    (runtime.Name.Equals(AsynchronousInstance.Name)))
                {
                    // Log.
                    //Logger.Instance.Log(
                    //    LogMessageCategory.Operational,
                    //    LogEventType.Information,
                    //    0,
                    //    AppTier.Workflow,
                    //    System.Reflection.MethodBase.GetCurrentMethod(),
                    //    null,
                    //    @"Asynchronous workflow runtime stopped : " + runtime.Name);

                    // Kill.
                    AsynchronousInstance.Dispose();
                    AsynchronousInstance = null;
                }
                else if ((SynchronousInstance != null) &
                    (runtime.Name.Equals(SynchronousInstance.Name)))
                {
                    // Log.
                    //Logger.Instance.Log(
                    //    LogMessageCategory.Operational,
                    //    LogEventType.Information,
                    //    0,
                    //    AppTier.Workflow,
                    //    System.Reflection.MethodBase.GetCurrentMethod(),
                    //    null,
                    //    @"Synchronous workflow runtime stopped : " + runtime.Name);

                    // Kill.
                    SynchronousInstance.Dispose();
                    SynchronousInstance = null;
                }
            }
        }

        #endregion Methods

        #region IDisposable Members

        internal static void Dispose()
        {
            if (AsynchronousInstance != null)
                AsynchronousInstance.Dispose();
            if (SynchronousInstance != null)
                SynchronousInstance.Dispose();
        }

        #endregion
    }
}
