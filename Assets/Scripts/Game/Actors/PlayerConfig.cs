using Krk.Bum.Common;
using UnityEngine;

namespace Krk.Bum.Game.Actors
{
    [CreateAssetMenu(menuName = "Krk/Game/Actors/Player")]
    public class PlayerConfig : ScriptableObject
    {
        public float WalkSpeed;
        public float MinTargetDistance;
        public FloatRange WalkRange;

        public float ReachRange;
    }
}
