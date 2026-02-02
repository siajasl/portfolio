using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Represents a type of error that may have occurred during processing.
    /// </summary>
    /// <remarks>
    /// Typically these are derived from a persistence control such as a database or xml file.
    /// </remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class ErrorType : 
        EntityBase
    {
        #region Ctor

        public ErrorType()
        {
            InitialiseMembers();
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// The processing error code.
        /// </summary>
        [DataMember()]
        public string Code
        { get; set; }

        /// <summary>
        /// The processing error code.
        /// </summary>
        public int CodeAsInt
        {
            get
            {
                int result = default(int);
                int.TryParse(Code, out result);
                return result;
            }
            set { Code = value.ToString().Trim(); }
        }

        /// <summary>
        /// Gets or sets the error code as an enumeration value.
        /// </summary>
        [DataMember()]
        public string CodeAsEnum
        { get; set; }

        /// <summary>
        /// Gets or sets the error severity.
        /// </summary>
        [DataMember()]
        public ErrorSeverityType Severity
        { get; set; }

        /// <summary>
        /// Gets or sets the error category.
        /// </summary>
        [DataMember()]
        public ErrorCategoryType Category
        { get; set; }

        /// <summary>
        /// The processing error log message category.
        /// </summary>
        [DataMember()]
        public string LogMessageCategory
        { get; set; }

        /// <summary>
        /// Sets the (unformatted) error message.
        /// </summary>
        [DataMember()]
        public string Message
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Standard member initialisation method.
        /// </summary>
        private void InitialiseMembers()
        {
            this.Category = ErrorCategoryType.Processing;
            this.Code = String.Empty;
            this.CodeAsEnum = string.Empty;
            this.LogMessageCategory = "General";
            this.Message = string.Empty;
            this.Severity = ErrorSeverityType.Medium;
        }

        #endregion Methods
    }
}