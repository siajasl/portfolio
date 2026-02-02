using System;
using System.Collections.Generic;

namespace Keane.CH.Framework.Apps.UI.Core
{
    /// <summary>
    /// Represents a command being invked across a gui.
    /// </summary>
    public class GuiCommand
    {
        #region Constructor

        public GuiCommand()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            this.IsVisible = true;
            this.IsEnabled = true;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the command id.
        /// </summary>
        /// <remarks>This must be unique across an application space.</remarks>
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or sets the command key.
        /// </summary>
        /// <remarks>This must be unique across an application space.</remarks>
        public string Key
        { get; set; }

        /// <summary>
        /// Gets or sets associated command data.
        /// </summary>
        public object Data
        { get; set; }

        /// <summary>
        /// Gets or sets gui context information.
        /// </summary>
        public GuiContext Context
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the command has 
        /// been cancelled at any point during it's lifetime.
        /// </summary>
        public bool IsCancelled
        { get; set; }

        /// <summary>
        /// Gets whether the command is curently enabled or not.
        /// </summary>
        public bool IsEnabled
        { get; internal set; }

        /// <summary>
        /// Gets whether the command is curently visible or not.
        /// </summary>
        public bool IsVisible
        { get; internal set; }

        #endregion Properties
    }
}
