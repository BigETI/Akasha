using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Button observer controller script class
    /// </summary>
    public class ButtonObserverControllerScript : MonoBehaviour, IButtonObserverController
    {
        /// <summary>
        /// Button controllers
        /// </summary>
        [SerializeField]
        private ButtonControllerScript[] buttonControllers = Array.Empty<ButtonControllerScript>();

        /// <summary>
        /// On all disabled
        /// </summary>
        [SerializeField]
        private UnityEvent onAllOff = default;

        /// <summary>
        /// On any on
        /// </summary>
        [SerializeField]
        private UnityEvent onAnyOn = default;

        /// <summary>
        /// On all on
        /// </summary>
        [SerializeField]
        private UnityEvent onAllOn = default;

        /// <summary>
        /// Button controllers
        /// </summary>
        public IReadOnlyList<ButtonControllerScript> ButtonControllers
        {
            get
            {
                if (buttonControllers == null)
                {
                    buttonControllers = Array.Empty<ButtonControllerScript>();
                }
                return buttonControllers;
            }
        }

        /// <summary>
        /// Last on button count
        /// </summary>
        private uint lastButtonOnCount;

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            uint button_on_count = 0U;
            foreach (ButtonControllerScript button_controller in ButtonControllers)
            {
                if (button_controller != null)
                {
                    if (button_controller.IsOn)
                    {
                        ++button_on_count;
                    }
                }
            }
            if (lastButtonOnCount != button_on_count)
            {
                uint last_button_on_count = lastButtonOnCount;
                lastButtonOnCount = button_on_count;
                if (button_on_count > 0U)
                {
                    if (last_button_on_count <= 0U)
                    {
                        if (onAnyOn != null)
                        {
                            onAnyOn.Invoke();
                        }
                    }
                    if (button_on_count >= ButtonControllers.Count)
                    {
                        if (onAllOn != null)
                        {
                            onAllOn.Invoke();
                        }
                    }
                }
                else
                {
                    if (onAllOff != null)
                    {
                        onAllOff.Invoke();
                    }
                }
            }
        }
    }
}
