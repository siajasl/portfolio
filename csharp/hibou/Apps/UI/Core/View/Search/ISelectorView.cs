using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.Core.View.Search
{
    /// <summary>
    /// View over a selector.
    /// </summary>
    public interface ISelectorView
    {
        /// <summary>
        /// Gets the selector type.
        /// </summary>
        string SelectorType { get; }

        /// <summary>
        /// Gets the selector sub-type.
        /// </summary>
        string SelectorSubType { get; }

        /// <summary>
        /// Gets the selector display type.
        /// </summary>
        SelectorDisplayType DisplayType { get; }

        /// <summary>
        /// Gets the selector sort direction type.
        /// </summary>
        SortDirectionType SortDirection { get; }

        /// <summary>
        /// Gets the selector sort control type.
        /// </summary>
        SelectorSortSourceType SortSourceType { get; }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        string DefaultValue { get; }

        ///// <summary>
        ///// Gets the default value control.
        ///// </summary>
        //SelectorDefaultValueSourceType DefaultValueSource { get; }

        /// <summary>
        /// Gets a flag indicating whether the null value will be suppressed.
        /// </summary>
        bool SuppressNullListItem { get; }

        /// <summary>
        /// Gets the associated list view.
        /// </summary>
        IListView List { get; }
    }

    /// <summary>
    /// Enuermation over the supported display types.
    /// </summary>
    public enum SelectorDisplayType
    {
        ValueAndDescription,
        Value,
        Description
    }

    /// <summary>
    /// Enuermation over the supported sort control types.
    /// </summary>
    public enum SelectorSortSourceType
    {
        Text,
        Value,
        OtherValue,
    }

    /// <summary>
    /// Enuermation over the supported default value control types.
    /// </summary>
    public enum SelectorDefaultValueSourceType
    {
        Ordinal,
        Value,
        Text,
    }

}
