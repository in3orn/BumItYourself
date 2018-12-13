using Krk.Bum.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class EndGameScreenView : ScreenView
    {
        public Button BackButton;

        [SerializeField]
        private Image awardImage = null;

        public void SetAwardImage(ImageData image)
        {
            awardImage.sprite = image.Image;
            awardImage.color = image.Color;
            awardImage.SetNativeSize();
        }
    }
}
