using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    [CreateAssetMenu(menuName = "Krk/View/Street/Part View")]
    public class PartViewConfig : ScriptableObject
    {
        public float FlyUpDuration;
        public float FlyDownDuration;
        public float LayDownDuration;
        public float DragDuration;

        public float StartYOffset;

        public FloatRange LayXRange;
        public FloatRange LayYRange;

        public FloatRange FlyYRange;

        public float FlyDuration { get { return FlyUpDuration + FlyDownDuration; } }
    }
}
