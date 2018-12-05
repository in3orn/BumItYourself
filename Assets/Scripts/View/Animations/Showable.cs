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
            if (parent != null) parent.SetActive(true);
        }

        protected void Deactivate()
        {
            if (parent != null) parent.SetActive(false);
        }
    }
}
