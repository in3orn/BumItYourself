using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    [CreateAssetMenu(menuName = "Krk/View/Buttons/Seller Item Button")]
    public class SellerItemButtonConfig : ScriptableObject
    {
        public string PriceFormat = "{0} $";

        public Color EquippedColor;
        public Color PurchasedColor;
        public Color DefaultColor;
        public Color LockedColor;
    }
}
