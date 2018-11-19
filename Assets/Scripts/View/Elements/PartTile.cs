using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Elements
{
    public class PartTile : MonoBehaviour
    {
        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI count = null;


        public void Init(PartData data)
        {
            count.text = data.Count.ToString();
            InitImage(data.Image);
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
