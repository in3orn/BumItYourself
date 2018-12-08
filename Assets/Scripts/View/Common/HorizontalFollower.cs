using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class HorizontalFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform target = null;

        [SerializeField]
        private FollowerConfig config = null;


        private void Update()
        {
            var targetX = target.position.x;
            var currentX = transform.position.x;
            var diff = targetX - currentX;
            if(Mathf.Abs(diff) > config.MinFollowDistance)
            {
                currentX = Mathf.Lerp(currentX, targetX, config.FollowStrength * Time.deltaTime);
                transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
            }
        }
    }
}
