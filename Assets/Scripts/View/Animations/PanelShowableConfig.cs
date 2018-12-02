using UnityEngine;

namespace Krk.Bum.View.Animations
{
    [CreateAssetMenu(menuName = "Krk/View/Animations/Panel Showable")]
    public class PanelShowableConfig : ScriptableObject
    {
        public float ShowDuration;
        public float HideDuration;

        public Vector2 ShowDirection;
    }
}
