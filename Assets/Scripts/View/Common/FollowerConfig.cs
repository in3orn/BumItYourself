using UnityEngine;

namespace Krk.Bum.View.Common
{
    [CreateAssetMenu(menuName = "Krk/View/Common/Follower")]
    public class FollowerConfig : ScriptableObject
    {
        public float MinFollowDistance;
        public float FollowStrength;
    }
}
