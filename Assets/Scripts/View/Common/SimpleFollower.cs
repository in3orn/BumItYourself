using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class SimpleFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform target = null;

        [SerializeField]
        private FollowerConfig config = null;


        private void LateUpdate()
        {
            Vector2 targetPos = target.position;
            Vector2 currentPos = transform.position;
            var diff = targetPos - currentPos;
            if (diff.magnitude > config.MinFollowDistance)
            {
                currentPos = Vector2.Lerp(currentPos, targetPos, config.FollowStrength);
                transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);
            }
        }
    }
}
