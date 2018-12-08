using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator = null;


        private Vector3 prevPosition;

        private bool left;


        private void Start()
        {
            prevPosition = transform.position;
        }

        private void Update()
        {
            var diff = transform.position - prevPosition;
            if (diff.x < -.1f) left = true;
            if (diff.x > .1f) left = false;

            animator.SetBool("left", left);
            animator.SetFloat("speed", diff.magnitude);

            prevPosition = transform.position;
        }


        public void Hit(Vector3 target)
        {
            var diff = target - transform.position;
            if (diff.x < -.1f) left = true;
            if (diff.x > .1f) left = false;

            animator.SetBool("left", left);
            animator.SetTrigger("Hit");
        }
    }
}
