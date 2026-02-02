using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Search.Contracts.Message;

namespace Keane.CH.Framework.Services.Reports.Contracts.Message
{
    /// <summary>
    /// Encapsulates get report data service operation response.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Reports")]
    [Serializable]
    public class GetReportDataResponse : 
        SearchResponse
    { }
}
