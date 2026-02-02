
namespace Keane.CH.Framework.Core.Utilities.Factory
{
    /// <summary>
    /// Encapsulates state initialisation operations.
    /// </summary>
    public interface IStateInitialiser
    {
        /// <summary>
        /// Sets the object's initial state.
        /// </summary>
        void InitialiseState();
    }
}