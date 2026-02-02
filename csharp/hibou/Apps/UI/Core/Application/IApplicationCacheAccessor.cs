using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Core.Utilities.Caching;

namespace Keane.CH.Framework.Apps.UI.Core.Application
{
    /// <summary>
    /// Exposes access to the application cache.
    /// </summary>
    public interface IApplicationCacheAccessor : 
        IEntityCacheAccessor
    { }
}