using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.Services.Reports.Contracts.Message
{
    /// <summary>
    /// Encapsulates information required to get report data.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Reports")]
    [Serializable]
    public class GetReportDataRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the search criteria used to drive the report.
        /// </summary>        
        [DataMember()]
        public SearchCriteriaBase Criteria
        { get; set; }

        #endregion Properties
    }
}
