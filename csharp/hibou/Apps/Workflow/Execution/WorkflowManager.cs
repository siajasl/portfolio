using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Workflow.Runtime;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Manages the execution of workflows in:
    ///     single or batch mode;
    ///     synchronously or asynchronously;
    ///     parallel or sequential.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public class WorkflowManager : IDisposable
    {
        #region Methods

        /// <summary>
        /// Executes the workflow instance.
        /// </summary>
        /// <param name="workflow">The workflow instance.</param>
        /// <param name="workflowThreadContext">The workflow thread context type.</param>
        public void ExecuteWorkflow(
            WorkflowInstance workflow,
            WorkflowThreadContextType workflowThreadContext)
        {
            // Defensive programming.
            if (workflow == null)
                throw new ArgumentNullException();

            // Instantiate a single item batch.
            WorkflowBatch batch = new WorkflowBatch() 
            { 
                ThreadContextType = workflowThreadContext,
                ExecutionType = WorkflowBatchExecutionType.Sequential,
                HostWorkflowInstanceId = workflow.InstanceId,
                HostWorkflowRuntimeId = workflow.WorkflowRuntime.Name
            };
            batch.AddWorkflow(workflow);
            
            // Execute the batch.
            ExecuteWorkflowBatch(batch);
        }

        /// <summary>
        /// Executes the workflow instance.
        /// </summary>
        /// <param name="workflow">The workflow instance.</param>
        /// <param name="workflowThreadContext">The workflow thread context type.</param>
        public void ExecuteWorkflow(
            Type type,
            Dictionary<string, object> parameters,
            WorkflowThreadContextType threadContext)
        {
            // Defensive programming.
            if (type == null)
                throw new ArgumentNullException("type");
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            // Instantiate & execute.
            WorkflowInstance workflow = 
                CreateWorkflow(type, parameters, threadContext);
            ExecuteWorkflow(workflow, threadContext);
        }

        /// <summary>
        /// Executes the workflow batch.
        /// </summary>
        /// <param name="batch">The workflow batch.</param>
        public void ExecuteWorkflowBatch(
            WorkflowBatch batch)
        {
            // Defensive programming.
            if (batch == null)
                throw new ArgumentNullException("batch");
            if (batch.Count <= 0)
                throw new ArgumentException("The workflow batch count is empty.");

            // Execute all the workflows within the batch.
            using (WorkflowBatchExecutor executor = new WorkflowBatchExecutor(batch))
            {
                executor.ExecuteBatch();
            }
        }

        /// <summary>
        /// Factory method to create a workflow.
        /// </summary>
        /// <param name="type">The type of workflow to be created.</param>
        /// <param name="parameters">The parameters to be passed into the workflow.</param>
        /// <param name="threadContext">The workflow thread context type.</param>
        /// <returns>A workflow instance.</returns>
        public WorkflowInstance CreateWorkflow(
            Type type,
            Dictionary<string, object> parameters,
            WorkflowThreadContextType threadContext)
        {
            // Defensive programming.
            if (type == null)
                throw new ArgumentNullException("type");
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            // Instantiate a workflow instance using the appropriate workflow runtime.
            WorkflowRuntime runtime =
                WorkflowRuntimeManager.GetRuntime(threadContext);
            WorkflowInstance result = 
                runtime.CreateWorkflow(type, parameters);
            return result;
        }

        #endregion Methods

        #region IDisposable implementation

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            WorkflowRuntimeManager.Dispose();
        }

        #endregion IDisposable implementation
    }
}