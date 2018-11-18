using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class ItemScreenView : ScreenView
    {
        public Button BackButton;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI itemName = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;


        public void Init(ItemData item)
        {
            itemName.text = item.Name;
            countLabel.text = "Count: " + item.Count;
            InitImage(item.Image);
        }

        private void InitImage(ImageData data) //TODO make some util method??
        {
            image.sprite = data.Image;
            image.color = data.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
            image.SetNativeSize();
        }
    }
}
