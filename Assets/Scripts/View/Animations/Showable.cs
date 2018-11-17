using UnityEngine;

namespace Krk.Bum.View.Animations
{
    public class Showable : MonoBehaviour
    {
        [SerializeField]
        protected GameObject parent = null;


        public virtual void Show()
        {
            Activate();
        }

        public virtual void Hide()
        {
            Deactivate();
        }


        protected void Activate()
        {
            parent.SetActive(true);
        }

        protected void Deactivate()
        {
            parent.SetActive(false);
        }
    }
}
