using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controlelrs namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Quantity selection UI controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class QuantitySelectionUIControllerScript : MonoBehaviour, IQuantitySelectionUIController
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [SerializeField]
        private int quantity = default;

        /// <summary>
        /// Minimal quantity
        /// </summary>
        [SerializeField]
        private int minimalQuantity = default;

        /// <summary>
        /// Maximal quantity
        /// </summary>
        [SerializeField]
        private int maximalQuantity = 100;

        /// <summary>
        /// Quantity input field
        /// </summary>
        [SerializeField]
        private TMP_InputField quantityInputField = default;

        /// <summary>
        /// On quantity changed
        /// </summary>
        [SerializeField]
        private UnityEvent<int> onQuantityChanged = default;

        /// <summary>
        /// Last quantity
        /// </summary>
        private int lastQuantity;

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity
        {
            get => Mathf.Clamp(quantity, MinimalQuantity, MaximalQuantity);
            set
            {
                int new_quantity = Mathf.Clamp(value, MinimalQuantity, MaximalQuantity);
                if (quantity != new_quantity)
                {
                    quantity = new_quantity;
                    UpdateInputField();
                    if (onQuantityChanged != null)
                    {
                        onQuantityChanged.Invoke(quantity);
                    }
                    OnQuantityChanged?.Invoke(quantity);
                }
            }
        }

        /// <summary>
        /// Minimal quantity
        /// </summary>
        public int MinimalQuantity
        {
            get => Mathf.Min(minimalQuantity, maximalQuantity);
            set => minimalQuantity = Mathf.Min(value, maximalQuantity);
        }

        /// <summary>
        /// Maximal quantity
        /// </summary>
        public int MaximalQuantity
        {
            get => Mathf.Max(minimalQuantity, maximalQuantity);
            set => maximalQuantity = Mathf.Max(value, maximalQuantity);
        }

        /// <summary>
        /// Quantity text
        /// </summary>
        public string QuantityText
        {
            get => Quantity.ToString();
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (int.TryParse(value, out int quantity))
                {
                    Quantity = quantity;
                }
            }
        }

        /// <summary>
        /// On quantity changed
        /// </summary>
        public event QuantityChangedDelegate OnQuantityChanged;

        /// <summary>
        /// Update input field
        /// </summary>
        private void UpdateInputField()
        {
            if (quantityInputField)
            {
                quantityInputField.SetTextWithoutNotify(Quantity.ToString());
            }
        }

        /// <summary>
        /// Decrement
        /// </summary>
        public void Decrement()
        {
            --Quantity;
        }

        /// <summary>
        /// Increment
        /// </summary>
        public void Increment()
        {
            ++Quantity;
        }

        /// <summary>
        /// Set quantity without notification
        /// </summary>
        /// <param name="quantity">QUantity</param>
        public void SetQuantityWithoutNotification(int quantity)
        {
            int new_quantity = Mathf.Clamp(quantity, MinimalQuantity, MaximalQuantity);
            if (this.quantity != new_quantity)
            {
                this.quantity = new_quantity;
                UpdateInputField();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            minimalQuantity = Mathf.Min(minimalQuantity, maximalQuantity);
            maximalQuantity = Mathf.Max(minimalQuantity, maximalQuantity);
            int new_quantity = Mathf.Clamp(quantity, minimalQuantity, maximalQuantity);
            if (lastQuantity != new_quantity)
            {
                lastQuantity = new_quantity;
                UpdateInputField();
            }
        }
    }
}
