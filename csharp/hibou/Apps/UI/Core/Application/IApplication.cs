using Keane.CH.Framework.Core.Utilities.Caching;

namespace Keane.CH.Framework.Apps.UI.Core.Application
{
    /// <summary>
    /// Represents an application.
    /// </summary>
    /// <typeparam name="C">The type of application cache in use.</typeparam>
    public interface IApplication
    {
        /// <summary>
        /// Application initiailsation routine.
        /// </summary>
        void Start();

        /// <summary>
        /// Application termination routine.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets or sets the associated application cache accessor.
        /// </summary>
        IApplicationCache Cache
        { get; set; }
    }
}