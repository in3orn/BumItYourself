using System;
using Krk.Bum.Model;
using Krk.Bum.View.Screen_Canvas;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class ItemButton : MonoBehaviour
    {
        public UnityAction<ItemButton> OnButtonClicked;


        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI count = null;

        [SerializeField]
        private NotificationView notification = null;


        public ItemData Item { get; private set; }


        private Color defaultCountColor;

        private void Awake()
        {
            defaultCountColor = count.color;
        }

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
            count.color = active ? defaultCountColor : Color.red;
        }

        public void SetNotificationShown(bool shown)
        {
            if (notification.Shown != shown)
            {
                if (!notification.Shown) notification.Show();
                else notification.Hide();
            }
        }
    }
}
