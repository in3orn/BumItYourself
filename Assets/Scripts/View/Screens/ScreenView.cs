using Krk.Bum.View.Animations;
using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField]
        protected GameObject screen = null;

        [SerializeField]
        protected Showable showable = null;


        private bool shown;


        public void InitShown(bool value)
        {
            shown = value;
            screen.SetActive(shown);
        }

        public void SetShown(bool value)
        {
            if (shown != value)
            {
                shown = value;
                if (shown) showable.Show();
                else showable.Hide();
            }
        }
    }
}
