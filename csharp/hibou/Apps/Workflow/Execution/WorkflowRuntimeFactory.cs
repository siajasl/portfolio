using System;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Encapsulates the creation of workflow runtimes.
    /// </summary>
    internal sealed class WorkflowRuntimeFactory
    {
        #region Constructor

        private WorkflowRuntimeFactory() { }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns a workflow runtime instance.
        /// </summary>
        /// <param name="threadContextType">the type of thread upon which the workflow's will run.</param>
        /// <returns>A workflow runtime instance.</returns>
        public static WorkflowRuntime Create(
            WorkflowThreadContextType threadContextType)
        {
            WorkflowRuntime runtimeInstance = new WorkflowRuntime();
            runtimeInstance.Name = Guid.NewGuid().ToString();
            ConfigureSchedulingService(runtimeInstance, threadContextType);
            return runtimeInstance;
        }

        /// <summary>
        /// Configures the scheduling service for the workflow runtime instance.
        /// </summary>
        /// <param name="runtimeInstance">The runtime instance being configured.</param>
        /// <param name="threadContextType">the type of thread upon which the workflow's will run.</param>
        private static void ConfigureSchedulingService(
            WorkflowRuntime runtimeInstance,
            WorkflowThreadContextType threadContextType)
        {
            WorkflowSchedulerService service = null;
            switch (threadContextType)
            {
                case WorkflowThreadContextType.Synch:
                    ManualWorkflowSchedulerService manualService =
                        new ManualWorkflowSchedulerService();
                    service = (WorkflowSchedulerService)manualService;
                    break;
                case WorkflowThreadContextType.Asynch:
                    DefaultWorkflowSchedulerService defaultService =
                        new DefaultWorkflowSchedulerService((int)(Environment.ProcessorCount * 20 * 0.8));
                    service = (WorkflowSchedulerService)defaultService;
                    break;
                default:
                    break;
            }
            if (service == null)
                throw new ApplicationException("Workflow runtime scheduling service could not be created.");
            runtimeInstance.AddService(service);
        }

        #endregion Methods
    }
}
