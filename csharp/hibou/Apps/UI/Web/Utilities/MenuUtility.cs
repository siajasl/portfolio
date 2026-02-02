using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;

namespace Keane.CH.Framework.Apps.UI.Web.Utilities
{
    /// <summary>
    /// Encapsulates menu utility methods.
    /// </summary>
    public sealed class MenuUtility
    {
        /// <summary>
        /// Indents the first menu item.
        /// </summary>
        /// <param name="menu">A navigation menu.</param>
        public static void IndentMenu(
            Menu menu, 
            uint indentSpaces)
        {
            IndentMenu(menu, indentSpaces, ContentDirection.LeftToRight);
        }

        /// <summary>
        /// Indents the first menu item.
        /// </summary>
        /// <param name="menu">A navigation menu.</param>
        public static void IndentMenu(
            Menu menu,
            uint indentSpaces,
            ContentDirection contentDirection)
        {
            // Defensively code.
            Debug.Assert(menu != null, "menu");
            int currentLength = menu.Items[0].Text.Length;
            int paddedLength = currentLength + (int)indentSpaces;
            if (contentDirection == ContentDirection.LeftToRight)                
                menu.Items[0].Text = menu.Items[0].Text.PadLeft(paddedLength);
            else
                menu.Items[0].Text = menu.Items[0].Text.PadRight(paddedLength);
        }

        /// <summary>
        /// Sets the current menu item based upon a simple site map walk algorithm.
        /// </summary>
        /// <param name="menu">The navigation menu.</param>
        /// <param name="provider">The site map provider.</param>
        public static void SetCurrentMenuItem(
            Menu menu,
            SiteMapProvider provider)
        {
            // Get the current sitemap node.
            if (provider == null)
                throw new ArgumentNullException("provider");
            SiteMapNode node =
                provider.FindSiteMapNode(HttpContext.Current);
            if (node == null)
                return;

            // Get the flattened list of menu items.
            List<MenuItem> menuItemList = new List<MenuItem>();
            SetMenuItems(menu.Items, ref menuItemList);
            if (menuItemList.Count == 0)
                return;

            // Select the first menu item that has a url matching 
            // either the current node or it's parent.
            do
            {
                // Select the menu item (if found).
                MenuItem item = GetMenuItemFromNode(menuItemList, node);
                if (item != null)
                {
                    item.Selected = true;
                    break;
                }

                // Walk to the parent node if feasible.
                if (node.ParentNode != null)
                    node = node.ParentNode;
                else
                    break;
            } while (node != null);
        }

        /// <summary>
        /// Recursively adds menu items to the passed list.
        /// </summary>
        /// <param name="menuItems">A collection of menu items.</param>
        /// <param name="flatlist">A flat list of menu items being generated.</param>
        private static void SetMenuItems(
            MenuItemCollection menuItemList,
            ref List<MenuItem> flatlist)
        {
            if ((menuItemList != null) && 
                (menuItemList.Count > 0))
            {
                foreach (MenuItem menuItem in menuItemList)
                {
                    flatlist.Add(menuItem);
                    if (menuItem.ChildItems.Count > 0)
                        SetMenuItems(menuItem.ChildItems, ref flatlist);
                }
            }
        }

        /// <summary>
        /// Returns the menu item with a url matching that of the passed site map node.
        /// </summary>
        /// <param name="flatMenuItemList">The flattened menu list.</param>
        /// <param name="node">The site map node.</param>
        /// <returns>The matching menu item if found.</returns>
        public static MenuItem GetMenuItemFromNode(
            List<MenuItem> flatMenuItemList,
            SiteMapNode node)
        {
            MenuItem result = null;
            if ((node != null) && (flatMenuItemList.Count > 0))
            {
                foreach (MenuItem item in flatMenuItemList)
                {
                    if (String.Equals(item.NavigateUrl.Trim(),
                                      node.Url,
                                      StringComparison.OrdinalIgnoreCase))
                    {
                        result = item;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Returns the menu item with a url matching that of the passed site map node.
        /// </summary>
        /// <param name="menu">The navigation menu.</param>
        /// <param name="node">The site map node.</param>
        /// <returns>The matching menu item if found.</returns>
        public static MenuItem GetMenuItemFromNode(
            Menu menu,
            SiteMapNode node)
        {
            MenuItem result = null;
            if ((node != null) && (menu.Items.Count > 0))
            {
                foreach (MenuItem item in menu.Items)
                {
                    if (String.Equals(item.NavigateUrl.Trim(),
                                      node.Url,
                                      StringComparison.OrdinalIgnoreCase))
                    {
                        result = item;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Builds a menu from a site map node list.
        /// </summary>
        /// <param name="menuItemList">The menu item collection to which new menu itmes will be added.</param>
        /// <param name="siteMapNodeList">The list of site mape nodes.</param>
        /// <param name="siteMapNodeForSelection">The site map node to be marked as selected.</param>
        /// <param name="obeyHierarchy">Flag indicating whether the menu hierarch is to be obeyed or not.</param>
        /// <param name="depth">The level of recursion.</param>
        public static void BuildMenu(
            MenuItemCollection menuItemList,
            SiteMapNodeCollection siteMapNodeList,
            SiteMapNode siteMapNodeForSelection,
            bool obeyHierarchy,
            ref int depth)
        {
            depth--;
            foreach (SiteMapNode siteMapNode in siteMapNodeList)
            {
                BuildMenu(
                    menuItemList, siteMapNode, siteMapNodeForSelection, obeyHierarchy, ref depth);
            }
        }

        /// <summary>
        /// Builds a menu from a site map node.
        /// </summary>
        /// <param name="menuItemList">The menu item collection to which new menu itmes will be added.</param>
        /// <param name="siteMapNode">The site map node to be added as a menu.</param>
        /// <param name="siteMapNodeForSelection">The site map node to be marked as selected.</param>
        /// <param name="obeyHierarchy">Flag indicating whether the menu hierarch is to be obeyed or not.</param>
        /// <param name="depth">The level of recursion.</param>
        public static void BuildMenu(
            MenuItemCollection menuItemList,
            SiteMapNode siteMapNode,
            SiteMapNode siteMapNodeForSelection,
            bool obeyHierarchy, 
            ref int depth)
        {
            // Build the menu item.
            MenuItem newMenuItem =
                BuildMenuItem(menuItemList, siteMapNode, siteMapNodeForSelection);

            // Recurse if instructed.
            if ((depth > 0) & (siteMapNode.HasChildNodes))
            {
                if (obeyHierarchy)
                    BuildMenu(newMenuItem.ChildItems, siteMapNode.ChildNodes, siteMapNodeForSelection, obeyHierarchy, ref depth);
                else
                    BuildMenu(menuItemList, siteMapNode.ChildNodes, siteMapNodeForSelection, obeyHierarchy, ref depth);
            }
        }

        /// <summary>
        /// Builds a menu item.
        /// </summary>
        /// <param name="menuItemList">The collection to which the item will be added.</param>
        /// <param name="siteMapNode">The site map node upon which the menu item will be based.</param>
        /// <param name="siteMapNodeForSelection">The site map node to be marked as selected.</param>
        /// <returns>A new menu item.</returns>
        private static MenuItem BuildMenuItem(
            MenuItemCollection menuItemList,
            SiteMapNode siteMapNode,
            SiteMapNode siteMapNodeForSelection)
        {
            // Instantiate the menu item.
            MenuItem result = null;
            result = new MenuItem() 
            {
                Text = siteMapNode.Description,
                NavigateUrl = siteMapNode.Url,            
            };
            
            // Make selected if appropriate.
            if (siteMapNodeForSelection != null)
            {
                result.Selected = 
                    siteMapNode.Url.Equals(siteMapNodeForSelection.Url);
            }

            // Add to the collection & return pointer.
            menuItemList.Add(result);
            return result;
        }
    }
}
