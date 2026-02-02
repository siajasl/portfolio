using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Keane.CH.Framework.DataAccess.ORM;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Manages the execution of search related stored procedures.
    /// </summary>
    internal sealed class SearchDaoSprocExecutor
    {
        /// <summary>
        /// Executes a search stored procedure.
        /// </summary>
        /// <typeparam name="C">A sub-class representing the search criteria.</typeparam>
        /// <typeparam name="I">A sub-class representing the search result.</typeparam>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The search dao configuration data.</param>
        /// <param name="criteria">The search criteria.</param>
        /// <returns>The results of the search as a list of items.</returns>
        internal static SearchResult Search<I>(
            IDao dao,
            SearchDaoConfiguration config,
            SearchCriteriaBase criteria)
            where I : new()
        {
            // Abort if execution conditions are not correct.
            if (dao == null)
                throw new ArgumentNullException("dao");
            if (config == null)
                throw new ArgumentNullException("config");
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            // Default result.
            SearchResult result = new SearchResult();

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add mapped parameters.
                    ORMapper.AddMappedParameterList(
                        config.CriteriaMappingList, db, cmd, criteria);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Iterate datareader.
                        while (dr.Read())
                        {
                            // Insantiate, map, & add to result.
                            I resultItem = new I();
                            ORMapper.Map(config.ResultMappingList, dr, resultItem);
                            result.Add(resultItem);
                        }
                    }
                }
            }
            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result;
        }
    }
}