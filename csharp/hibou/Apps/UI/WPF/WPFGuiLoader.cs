using System;
using System.Windows.Controls;
using Keane.CH.Framework.Apps.UI.WPF.ExtensionMethods;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Encapsulates WPF gui loading.
    /// </summary>
    internal sealed class WPFGuiLoader
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
        /// <remarks>This is used in WPF windows.</remarks>
        /// <param name="control">A UI control.</param>
        internal static void Reload(Control control)
        {
            if (control != null)
            {
                // Reload state.
                IWPFGuiContainer container = (control as IWPFGuiContainer);
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
                IWPFGuiContainer container = (control as IWPFGuiContainer);
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
                IWPFGuiContainer container = (control as IWPFGuiContainer);
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
                IWPFGuiContainer container = (control as IWPFGuiContainer);
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
