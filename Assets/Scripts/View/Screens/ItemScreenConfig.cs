using UnityEngine;

namespace Krk.Bum.View.Screens
{
    [CreateAssetMenu(menuName = "Krk/View/Screens/Item Screen")]
    public class ItemScreenConfig : ScriptableObject
    {
        public Color CreateButtonActive;
        public Color CreateButtonLocked;

        [Header("First Show Animation")]
        public float FirstBackShowDuration;
        public float FirstItemShowDuration;

        public Vector2 FirstBackShowScale;
        public Vector2 FirstItemShowScale;

        [Header("First Action Animation")]
        public float FirstBackActionDuration;
        public float FirstItemActionDuration;

        public float FirstBackRotationStrength;
        public int FirstBackRotationVibrato;

        [Header("First Hide Animation")]
        public float FirstHideDelay;

        public float FirstBackHideDuration;
        public float FirstItemHideDuration;
    }
}
