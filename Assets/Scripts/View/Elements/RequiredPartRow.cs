using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Elements
{
    public class RequiredPartRow : MonoBehaviour
    {
        [SerializeField]
        private RequiredPartRowConfig config = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI nameLabel = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;


        public void Init(RequiredPartData data)
        {
            nameLabel.text = data.Name;
            countLabel.text = string.Format(config.CountFormat, data.Count, data.RequiredCount);
            InitImage(data.Image);

            if (data.Count >= data.RequiredCount)
            {
                countLabel.color = config.AvailableColor;
            }
            else
            {
                countLabel.color = config.UnavailableColor;
            }
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
