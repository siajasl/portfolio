using System;
using System.Diagnostics;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Web;

namespace Keane.CH.Framework.Apps.UI.Web.Specialized
{
    /// <summary>
    /// Represents a base page from which all error related pages inherit.
    /// </summary>
    public class ErrorPageBase :
        WebPageBase
    {
        /// <summary>
        /// Returns the session fault that has caused the redirect.
        /// </summary>
        protected Exception SessionFault
        {
            get 
            {
                Exception result = null;
                if (Session[@"SessionFault"] != null)
                    result = Session[@"SessionFault"] as Exception;
                return result;
            }
            set { Session[@"SessionFault"] = value;}
        }

        /// <summary>
        /// Executed when the page load cycle has completed.
        /// </summary>
        public override void OnGuiLoaded()
        {
            base.OnGuiLoaded();
            // Clear the fault from the session cache.
            SessionFault = null;
            Session.Remove(@"SessionFault");
        }
    }
}