using Krk.Bum.View.Animations;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField]
        protected GameObject screen = null;

        [SerializeField]
        protected Showable[] showables = null;


        private bool shown;


        public virtual void InitShown(bool value)
        {
            shown = value;
            screen.SetActive(shown);
        }

        public virtual void SetShown(bool value)
        {
            if (shown != value)
            {
                shown = value;
                if (shown)
                {
                    foreach (var showable in showables) showable.Show();
                }
                else
                {
                    foreach (var showable in showables) showable.Hide();
                }
            }
        }
    }
}
