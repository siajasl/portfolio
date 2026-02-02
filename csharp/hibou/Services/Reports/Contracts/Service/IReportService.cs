using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Reports.Contracts.Message;
using Keane.CH.Framework.Services.Search.Contracts.Message;

namespace Keane.CH.Framework.Services.Reports.Contracts
{
    /// <summary>
    /// Report specific search service interface.
    /// </summary>
    /// <typeparam name="RI">The report item.</typeparam>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Reports")]
    public interface IReportService
    {
        /// <summary>
        /// Returns the report related data.
        /// </summary>
        /// <param name="request">The search criteria.</param>
        /// <returns>Report related data.</returns>
        [OperationContract()]
        GetReportDataResponse GetReportData(GetReportDataRequest request);
    }
}