using Akasha.InputActions;
using System;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Input controller interface
    /// </summary>
    public interface IInputController : IBehaviour
    {
        /// <summary>
        /// Game input actions
        /// </summary>
        GameInputActions GameInputActions { get; }

        /// <summary>
        /// On any key pressed
        /// </summary>
        event Action OnAnyKeyPressed;

        /// <summary>
        /// On back key pressed
        /// </summary>
        event Action OnBackKeyPressed;

        /// <summary>
        /// On cancel key pressed
        /// </summary>
        event Action OnCancelKeyPressed;

        /// <summary>
        /// On forward key pressed
        /// </summary>
        event Action OnForwardKeyPressed;

        /// <summary>
        /// On menu key pressed
        /// </summary>
        event Action OnMenuKeyPressed;

        /// <summary>
        /// On modifier key pressed
        /// </summary>
        event Action OnModifierKeyPressed;

        /// <summary>
        /// On primary action key pressed
        /// </summary>
        event Action OnPrimaryActionKeyPressed;

        /// <summary>
        /// On primary trigger key pressed
        /// </summary>
        event Action OnPrimaryTriggerKeyPressed;

        /// <summary>
        /// On secondary action key pressed
        /// </summary>
        event Action OnSecondaryActionKeyPressed;

        /// <summary>
        /// On secondary trigger key pressed
        /// </summary>
        event Action OnSecondaryTriggerKeyPressed;

        /// <summary>
        /// On submit key pressed
        /// </summary>
        event Action OnSubmitKeyPressed;
    }
}
