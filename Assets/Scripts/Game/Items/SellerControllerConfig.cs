using UnityEngine;

namespace Krk.Bum.Game.Items
{
    [CreateAssetMenu(menuName = "Krk/Game/Items/Seller Controller")]
    public class SellerControllerConfig : ScriptableObject
    {
        public SellerGroupData[] Groups;
    }
}
