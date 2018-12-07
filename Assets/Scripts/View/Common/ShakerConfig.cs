using UnityEngine;

namespace Krk.Bum.View.Common
{
    [CreateAssetMenu(menuName = "Krk/View/Common/Shaker")]
    public class ShakerConfig : ScriptableObject
    {
        public float Duration;
        public float Strength;
        public int Vibrato;
    }
}
