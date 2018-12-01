using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    [CreateAssetMenu(menuName = "Krk/View/Buttons/Store Item Button")]
    public class StoreItemButtonConfig : ScriptableObject
    {
        public Color DefaultColor;
        public Color LockedColor;
    }
}
