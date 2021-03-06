﻿using Krk.Bum.Model;
using Krk.Bum.View.Screen_Canvas;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class CollectionItemButton : MonoBehaviour
    {
        public UnityAction<CollectionItemButton> OnButtonClicked;


        [SerializeField]
        private CollectionItemButtonConfig config = null;

        [SerializeField]
        private Button button = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private Image background = null;

        [SerializeField]
        private TextMeshProUGUI count = null;

        [SerializeField]
        private NotificationView notification = null;


        public ItemData Item { get; private set; }
        

        public void Init(ItemData data)
        {
            Item = data;
            UpdateAppearance();
        }

        private void InitImage(ImageData data) //TODO make some util method??
        {
            image.sprite = data.Image;
            image.color = data.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
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
            count.text = Item.Count.ToString();
            if (Item.TotalCount > 0)
            {
                InitImage(Item.Image);
            }
        }

        public void SetNotificationShown(bool shown)
        {
            if (notification.Shown != shown)
            {
                if (!notification.Shown) notification.Show();
                else notification.Hide();
            }

            background.color = shown ? config.DefaultColor : config.LockedColor;
        }
    }
}
