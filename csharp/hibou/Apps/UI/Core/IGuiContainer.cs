
namespace Keane.CH.Framework.Apps.UI.Core
{
    /// <summary>
    /// Represents a gui container that may or may not contain child controls.
    /// </summary>
    public interface IGuiContainer
    {
        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        void OnGuiInjectDependencies();

        /// <summary>
        /// Pre initial load event (fired the first time the gui is loaded).
        /// </summary>
        void OnGuiLoading();

        /// <summary>
        /// Initial load event (fired the first time the gui is loaded).
        /// </summary>
        void OnGuiLoad();

        /// <summary>
        /// Post initial load event (fired the first time the gui is loaded).
        /// </summary>
        void OnGuiLoaded();

        /// <summary>
        /// Reset event (fired before load events).
        /// </summary>
        void OnGuiReset();

        /// <summary>
        /// Gui lock event (locks or unlocks the gui input state thereby controlling the ability of a user to interact with the view).
        /// </summary>
        /// <param name="locked">Flag indicating whether the gui will be locked for user input.</param>
        void OnGuiLock(bool locked);

        /// <summary>
        /// Sets the command state across the container.
        /// </summary>
        /// <param name="command">The command.</param>
        void OnGuiSetCommandState(GuiCommand command);
    }
}