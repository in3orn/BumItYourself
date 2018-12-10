using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    [CreateAssetMenu(menuName = "Krk/View/Buttons/Collection Item Button")]
    public class CollectionItemButtonConfig : ScriptableObject
    {
        public Color DefaultColor;
        public Color LockedColor;
    }
}
