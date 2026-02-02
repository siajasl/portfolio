using System.Collections.Generic;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Core.View;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Extends the Keane.CH.Framework.Apps.UI.Web.IWebGuiContainer.
    /// </summary>
    public static class IWebGuiContainerExtensions
    {
        /// <summary>
        /// Returns a container's base type.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static IWebGuiContainerType GetContainerType(
            this IWebGuiContainer container)
        {
            WebPageBase page = container as WebPageBase;
            if (page != null)
                return IWebGuiContainerType.Page;
            WebUserControlBase userControl = container as WebUserControlBase;
            if (userControl != null)
                return IWebGuiContainerType.UserControl;
            WebMasterPageBase masterPage = container as WebMasterPageBase;
            if (masterPage != null)
                return IWebGuiContainerType.MasterPage;
            return IWebGuiContainerType.Unknown;
        }

        /// <summary>
        /// Returns a container's child controls.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static ControlCollection GetControlCollection(
            this IWebGuiContainer container)
        {
            ControlCollection result = null;
            switch (container.GetContainerType())
            {
                case IWebGuiContainerType.Page:
                    result = container.AsPage().Controls;
                    break;
                case IWebGuiContainerType.UserControl:
                    result = container.AsUserControl().Controls;
                    break;
                case IWebGuiContainerType.MasterPage:
                    result = container.AsMasterPage().Controls;
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns a generic list of the container's child controls.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static List<Control> GetControlList(
            this IWebGuiContainer container)
        {
            List<Control> result = new List<Control>();
            ControlCollection controls = container.GetControlCollection();
            if (controls != null)
            {
                foreach (Control item in controls)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a flag indicatin whether the container should be automatically locked.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static bool SupportsAutoLocking(
            this IWebGuiContainer container)
        {
            return (container.GetEditMode() == EditModeType.View);
        }

        /// <summary>
        /// Returns the container's edit mode.
        /// </summary>
        /// <param name="container">The container in question.</param>
        internal static EditModeType GetEditMode(
            this IWebGuiContainer container)
        {
            EditModeType result;            
            switch (container.GetContainerType())
            {
                case IWebGuiContainerType.Page:
                    result = EditModeType.Unspecified;
                    break;
                case IWebGuiContainerType.UserControl:
                    result = container.AsUserControl().EditMode;
                    break;
                case IWebGuiContainerType.MasterPage:
                    result = EditModeType.Unspecified;
                    break;
                default:
                    result = EditModeType.Unspecified;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the container casted to WebPageBase.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WebPageBase AsPage(
            this IWebGuiContainer container)
        {
            WebPageBase result = null;
            if (container.GetContainerType() == IWebGuiContainerType.Page)
                result = (WebPageBase)container;
            return result;
        }

        /// <summary>
        /// Returns the container casted to WebUserControlBase.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WebUserControlBase AsUserControl(
            this IWebGuiContainer container)
        {
            WebUserControlBase result = null;
            if (container.GetContainerType() == IWebGuiContainerType.UserControl)
                result = (WebUserControlBase)container;
            return result;
        }

        /// <summary>
        /// Returns the container casted to WebMasterPageBase.
        /// </summary>
        /// <param name="container">The container in question.</param>
        private static WebMasterPageBase AsMasterPage(
            this IWebGuiContainer container)
        {
            WebMasterPageBase result = null;
            if (container.GetContainerType() == IWebGuiContainerType.MasterPage)
                result = (WebMasterPageBase)container;
            return result;
        }
    }
}
