using System.Data;
using Keane.CH.Framework.Core.Utilities.Caching;

namespace Keane.CH.Framework.Apps.UI.Core.Application
{
    /// <summary>
    /// Manages the application's local cache.
    /// </summary>
    public interface IApplicationCache
    {
        /// <summary>
        /// Initialises the cache.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Resets the cache.
        /// </summary>
        void Reset();

        /// <summary>
        /// Gets the associated cache accessor.
        /// </summary>
        IApplicationCacheAccessor Accessor
        { get; }
    }
}