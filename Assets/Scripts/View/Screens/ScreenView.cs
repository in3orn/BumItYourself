using UnityEngine;

namespace Krk.Bum.View.Buttons
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField]
        private GameObject screen = null;


        public void InitShown(bool shown)
        {
            screen.SetActive(shown);
        }

        public void SetShown(bool shown)
        {
            screen.SetActive(shown);
        }
    }
}
