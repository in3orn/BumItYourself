using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;


        public void Hit()
        {
            animator.SetTrigger("Hit");
        }
    }
}
