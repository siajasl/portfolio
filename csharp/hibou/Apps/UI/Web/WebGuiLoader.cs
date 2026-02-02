using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Web.ExtensionMethods;
using Keane.CH.Framework.Apps.UI.Web.AjaxResponse;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Encapsulates web gui loading.
    /// </summary>
    internal sealed class WebGuiLoader
    {
        /// <summary>
        /// Loads a gui.
        /// </summary>
        /// <param name="control">A UI control.</param>
        internal static void Load(Control control)
        {
            // Defensive programming.
            if (control == null)
                throw new ArgumentNullException("control");

            // 3 phase load.
            DoPreLoad(control);
            DoLoad(control);
            DoPostLoad(control);
        }

        /// <summary>
        /// Reloads a gui.
        /// </summary>
        /// <remarks>This is used in web pages.</remarks>
        /// <param name="control">A UI control.</param>
        internal static void Reload(Control control)
        {
            if (control != null)
            {
                // Reload state.
                IWebGuiContainer container = (control as IWebGuiContainer);
                if (container != null)
                {
                    container.OnGuiReload();
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => Reload(c));
            }
        }

        /// <summary>
        /// Performs the pre-load.
        /// </summary>
        /// <param name="control">A UI control.</param>
        private static void DoPreLoad(Control control)
        {
            if (control != null) 
            {
                // Publish pre-event.
                IWebGuiContainer container = (control as IWebGuiContainer);
                if (container != null)
                {
                    container.OnGuiLoading();
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => DoPreLoad(c));
            }
        }

        /// <summary>
        /// Performs the load.
        /// </summary>
        /// <param name="control">A UI control.</param>
        private static void DoLoad(Control control)
        {
            if (control != null)
            {
                // Publish pre-event.
                IWebGuiContainer container = (control as IWebGuiContainer);
                if (container != null)
                {
                    container.OnGuiLoad();
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => DoLoad(c));
            }
        }

        /// <summary>
        /// Performs the post-load.
        /// </summary>
        /// <param name="control">A UI control.</param>
        private static void DoPostLoad(Control control)
        {
            if (control != null)
            {
                // Publish pre-event.
                IWebGuiContainer container = (control as IWebGuiContainer);
                if (container != null)
                {
                    container.OnGuiLoaded();
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => DoPostLoad(c));
            }
        }
    }
}