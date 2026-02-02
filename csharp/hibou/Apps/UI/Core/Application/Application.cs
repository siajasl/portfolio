using Keane.CH.Framework.Core.Utilities.Caching;

namespace Keane.CH.Framework.Apps.UI.Core.Application
{
    /// <summary>
    /// Represents an application manager that performs various application level tasks.
    /// </summary>
    public class Application :
        IApplication
    {
        #region IApplicationManager Members

        /// <summary>
        /// Application initiailsation routine.
        /// </summary>
        public void Start()
        {
            // Initialise local cache.
            if (this.Cache != null)
                this.Cache.Initialise();
        }

        /// <summary>
        /// Application termination routine.
        /// </summary>
        public void Stop()
        {
            // Clear local cache.
            if (this.Cache != null)
                this.Cache.Clear();
        }

        /// <summary>
        /// Gets or sets the associated application cache accessor.
        /// </summary>
        public IApplicationCache Cache
        { 
            get; 
            set; 
        }

        #endregion IApplicationManager Members
    }
}
