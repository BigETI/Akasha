/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Quantity selection UI controller interface
    /// </summary>
    public interface IQuantitySelectionUIController : IBehaviour
    {
        /// <summary>
        /// Quantity
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Minimal quantity
        /// </summary>
        int MinimalQuantity { get; set; }

        /// <summary>
        /// Maximal quantity
        /// </summary>
        int MaximalQuantity { get; set; }

        /// <summary>
        /// Quantity text
        /// </summary>
        string QuantityText { get; set; }

        /// <summary>
        /// On quantity changed
        /// </summary>
        event QuantityChangedDelegate OnQuantityChanged;

        /// <summary>
        /// Decrement
        /// </summary>
        void Decrement();

        /// <summary>
        /// Increment
        /// </summary>
        void Increment();

        /// <summary>
        /// Set quantity without notification
        /// </summary>
        /// <param name="quantity">QUantity</param>
        void SetQuantityWithoutNotification(int quantity);
    }
}
