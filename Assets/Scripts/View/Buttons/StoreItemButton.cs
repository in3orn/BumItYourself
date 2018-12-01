using Krk.Bum.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class StoreItemButton : MonoBehaviour
    {
        public UnityAction<StoreItemButton> OnButtonClicked;


        [SerializeField]
        private StoreItemButtonConfig config = null;

        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI count = null;


        public ItemData Item { get; private set; }


        public void Init(ItemData data)
        {
            Item = data;
            UpdateAppearance();
            InitImage(data.Image);
        }

        private void InitImage(ImageData data) //TODO make some util method??
        {
            image.sprite = data.Image;
            image.color = data.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
            image.SetNativeSize();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(HandleButtonClicked);
            }
        }

        private void HandleButtonClicked()
        {
            OnButtonClicked?.Invoke(this);
        }

        public void UpdateAppearance()
        {
            var active = Item.Count > 0;
            count.text = Item.Count.ToString();
            count.color = active ? config.DefaultColor : config.LockedColor;
            button.interactable = active;
        }
    }
}
