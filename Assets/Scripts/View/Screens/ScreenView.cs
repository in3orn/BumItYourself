using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField]
        private GameObject screen;


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
