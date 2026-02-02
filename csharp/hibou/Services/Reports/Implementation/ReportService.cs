using System;
using System.Linq;
using Keane.CH.Framework.DataAccess.Search;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Core;
using Keane.CH.Framework.Services.Reports.Contracts;
using Keane.CH.Framework.Services.Reports.Contracts.Message;
using Keane.CH.Framework.Services.Search.Contracts.Message;

namespace Keane.CH.Framework.Services.Reports.Implementation
{
    /// <summary>
    /// Report specific service base class for encapsulating common functionality.
    /// </summary>
    /// <typeparam name="RI">The type of report item returned by the search.</typeparam>
    public class ReportService<RI> :
        ServiceImplementationBase,
        IReportService
        where RI : new()
    {
        #region Properties

        #region Collaborators

        /// <summary>
        /// Gets or sets the associated search dao.
        /// </summary>
        public ISearchDao SearchDao
        { get; set; }

        #endregion Collaborators

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retursn the report related data.
        /// </summary>
        /// <param name="request">The search criteria.</param>
        /// <returns>Search results.</returns>
        public GetReportDataResponse
            GetReportData(GetReportDataRequest request)
        {
            try
            {
                // Search the repository.
                SearchResult searchResult = SearchDao.Search(request.Criteria);

                // Restrict the number to be returned.
                searchResult.SetMaximumSize(request.Criteria.MaximumResults);

                // TODO caclulate time.

                // Generate service response.
                GetReportDataResponse response = new GetReportDataResponse();
                response.Status = OperationResponseStatus.Success;
                response.Result = searchResult;
                return response;
            }
            catch (Exception ex)
            {
                GetReportDataResponse response = new GetReportDataResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        #endregion Methods
    }
}