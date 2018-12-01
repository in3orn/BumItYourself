using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    [CreateAssetMenu(menuName = "Krk/View/Screen Canvas/Count Display")]
    public class CountDisplayConfig : ScriptableObject
    {
        public float ShowDelay;
        public float ShowDuration;

        public float IncreaseDuration;
        public float DecreaseDuration;
    }
}
