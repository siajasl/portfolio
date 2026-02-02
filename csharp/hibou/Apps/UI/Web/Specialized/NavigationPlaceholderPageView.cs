using System.Web;
using Keane.CH.Framework.Apps.UI.Web;

namespace Keane.CH.Framework.Apps.UI.Web.Specialized
{
    /// <summary>
    /// Helper base page sub-classed by pages acting as navigation containers.
    /// </summary>
    public class WebNavigationPlaceholderPage : 
        WebPageBase
    {
        #region PageBase overrides

        /// <summary>
        /// Initial load event.
        /// </summary>
        /// <remarks>
        /// Occurs on the first time the page is loaded.
        /// </remarks>
        public override void OnGuiLoad()
        {
            if (SiteMap.CurrentNode != null)
            {
                if (SiteMap.CurrentNode.HasChildNodes)
                {
                    Response.Redirect(ResolveUrl(SiteMap.CurrentNode.ChildNodes[0].Url));
                }
                else if (SiteMap.CurrentNode.ParentNode != null)
                {
                    Response.Redirect(ResolveUrl(SiteMap.CurrentNode.ParentNode.Url));
                }
            }
        }

        #endregion PageBase overrides
    }
}