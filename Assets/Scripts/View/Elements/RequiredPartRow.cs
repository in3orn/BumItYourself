using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Elements
{
    public class RequiredPartRow : MonoBehaviour
    {
        [SerializeField]
        private RequiredPartRowConfig config;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI nameLabel = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;


        private Color defaultNameColor;
        private Color defaultCountColor;


        private void Awake()
        {
            defaultNameColor = nameLabel.color;
            defaultCountColor = countLabel.color;
        }


        public void Init(RequiredPartData data)
        {
            nameLabel.text = data.Name;
            countLabel.text = string.Format(config.CountFormat, data.Count, data.RequiredCount);
            InitImage(data.Image);

            if (data.Count >= data.RequiredCount)
            {
                nameLabel.color = config.SuccessColor;
                countLabel.color = config.SuccessColor;
            }
            else
            {
                nameLabel.color = defaultNameColor;
                countLabel.color = defaultCountColor;
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
