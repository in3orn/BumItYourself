using UnityEngine;

namespace Krk.Bum.View.Common
{
    [CreateAssetMenu(menuName ="Krk/View/Common/Flash")]
    public class FlashConfig : ScriptableObject
    {
        public float FadeInDuration;
        public float FadeOutDuration;
    }
}
