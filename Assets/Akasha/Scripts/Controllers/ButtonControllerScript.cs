using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Disatser controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Button controller script class
    /// </summary>
    public class ButtonControllerScript : InteractableControllerScript, IButtonController
    {
        /// <summary>
        /// Is on
        /// </summary>
        [SerializeField]
        private bool isOn;

        /// <summary>
        /// Is a switch
        /// </summary>
        [SerializeField]
        private bool isASwitch;

        /// <summary>
        /// Hold press time in seconds (0 for forever)
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float holdPressTime = 0.0f;

        /// <summary>
        /// On switch on
        /// </summary>
        [SerializeField]
        private UnityEvent onSwitchOn = default;

        /// <summary>
        /// On switch off
        /// </summary>
        [SerializeField]
        private UnityEvent onSwitchOff = default;

        /// <summary>
        /// Elapsed hold press time in seconds
        /// </summary>
        private float elapsedHoldPressTime;

        /// <summary>
        /// Last is on
        /// </summary>
        private bool lastIsOn;

        /// <summary>
        /// Is on
        /// </summary>
        public bool IsOn
        {
            get => isOn;
            set => isOn = value;
        }

        /// <summary>
        /// Is a switch
        /// </summary>
        public bool IsASwitch
        {
            get => isASwitch;
            set => isASwitch = value;
        }

        /// <summary>
        /// Hold press time in seconds (0 for forever)
        /// </summary>
        public float HoldPressTime
        {
            get => Mathf.Max(holdPressTime, 0.0f);
            set => holdPressTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Interact
        /// </summary>
        public override void Interact()
        {
            if (isASwitch)
            {
                isOn = !isOn;
                lastIsOn = isOn;
                elapsedHoldPressTime = 0.0f;
                if (isOn)
                {
                    lastIsOn = true;
                    if (onSwitchOn != null)
                    {
                        onSwitchOn.Invoke();
                    }
                }
                else if (onSwitchOff != null)
                {
                    onSwitchOff.Invoke();
                }
                base.Interact();
            }
            else if (!isOn)
            {
                isOn = true;
                lastIsOn = true;
                elapsedHoldPressTime = 0.0f;
                if (onSwitchOn != null)
                {
                    onSwitchOn.Invoke();
                }
                base.Interact();
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (isOn)
            {
                lastIsOn = true;
                if (onSwitchOn != null)
                {
                    onSwitchOn.Invoke();
                }
            }
            else if (onSwitchOff != null)
            {
                onSwitchOff.Invoke();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (lastIsOn != isOn)
            {
                lastIsOn = isOn;
                if (isOn)
                {
                    if (onSwitchOn != null)
                    {
                        onSwitchOn.Invoke();
                    }
                }
                else if (onSwitchOff != null)
                {
                    onSwitchOff.Invoke();
                }
            }
            if (isOn)
            {
                float hold_press_time = HoldPressTime;
                if (hold_press_time > float.Epsilon)
                {
                    elapsedHoldPressTime += Time.deltaTime;
                    if (elapsedHoldPressTime >= hold_press_time)
                    {
                        isOn = false;
                        lastIsOn = false;
                        elapsedHoldPressTime = 0.0f;
                        if (onSwitchOff != null)
                        {
                            onSwitchOff.Invoke();
                        }
                    }
                }
            }
            else
            {
                elapsedHoldPressTime = 0.0f;
            }
        }
    }
}
