using System.Collections.Generic;
using System.Windows.Controls;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.WPF.Utilities;
using Keane.CH.Framework.Apps.UI.WPF.ExtensionMethods;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Extends the Keane.CH.Framework.Apps.UI.WPF.IWPFGuiContainer.
    /// </summary>
    public static class WPFGuiContainerExtensions
    {
        /// <summary>
        /// Returns a container's base type.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WPFGuiContainerType GetContainerType(
            this IWPFGuiContainer container)
        {
            WPFWindowBase window = container as WPFWindowBase;
            if (window != null)
                return WPFGuiContainerType.Window;
            WPFUserControlBase userControl = container as WPFUserControlBase;
            if (userControl != null)
                return WPFGuiContainerType.UserControl;
            return WPFGuiContainerType.Unknown;
        }

        /// <summary>
        /// Returns a container's child controls.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static List<Control> GetControlCollection(
            this IWPFGuiContainer container)
        {
            List<Control> result = null;
            switch (container.GetContainerType())
            {
                case WPFGuiContainerType.Window:
                    result = ((Control)container.AsWindow()).GetChildControlList(false);
                    break;
                case WPFGuiContainerType.UserControl:
                    result = ((Control)container.AsUserControl()).GetChildControlList(false);
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns a flag indicatin whether the container should be automatically locked.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static bool SupportsAutoLocking(
            this IWPFGuiContainer container)
        {
            return (container.GetEditMode() == EditModeType.View);
        }

        /// <summary>
        /// Returns the container's edit mode.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static EditModeType GetEditMode(
            this IWPFGuiContainer container)
        {
            EditModeType result;
            switch (container.GetContainerType())
            {
                case WPFGuiContainerType.Window:
                    result = EditModeType.Unspecified;
                    break;
                case WPFGuiContainerType.UserControl:
                    result = container.AsUserControl().EditMode;
                    break;
                default:
                    result = EditModeType.Unspecified;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the container casted to WPFWindowBase.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WPFWindowBase AsWindow(
            this IWPFGuiContainer container)
        {
            WPFWindowBase result = null;
            if (container.GetContainerType() == WPFGuiContainerType.Window)
                result = (WPFWindowBase)container;
            return result;
        }

        /// <summary>
        /// Returns the container casted to WPFUserControlBase.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WPFUserControlBase AsUserControl(
            this IWPFGuiContainer container)
        {
            WPFUserControlBase result = null;
            if (container.GetContainerType() == WPFGuiContainerType.UserControl)
                result = (WPFUserControlBase)container;
            return result;
        }
    }
}