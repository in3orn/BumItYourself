using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using Krk.Bum.View.Screen_Canvas;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class CollectionView : MonoBehaviour
    {
        public UnityAction<bool> OnExpanded;


        [SerializeField]
        private TextMeshProUGUI nameLabel = null;

        [SerializeField]
        private CollectionItemButton itemButton = null;

        [SerializeField]
        private RectTransform header = null;

        [SerializeField]
        private RectTransform itemsContent = null;

        [SerializeField]
        private NotificationView notification = null;

        [SerializeField]
        private RectTransform expander = null;

        [SerializeField]
        private Button expanderButton = null;


        private bool expanded;


        private readonly List<CollectionItemButton> itemButtons;

        public List<CollectionItemButton> ItemButtons { get { return itemButtons; } }


        public CollectionView()
        {
            itemButtons = new List<CollectionItemButton>();
        }

        private void OnEnable()
        {
            expanderButton.onClick.AddListener(HandleExpanderButtonClicked);
        }

        private void OnDisable()
        {
            expanderButton.onClick.RemoveListener(HandleExpanderButtonClicked);
        }

        private void HandleExpanderButtonClicked()
        {
            expanded = !expanded;
            itemsContent.gameObject.SetActive(expanded);
            expander.rotation = Quaternion.Euler(0f, 0f, expanded ? -180f : -90f);

            if (OnExpanded != null) OnExpanded(expanded);
        }

        public void Init(CollectionData collection)
        {
            nameLabel.text = collection.Name;

            CreateItems(collection.Items);
        }

        private void CreateItems(ItemData[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var gameObject = Instantiate(itemButton, itemsContent);
                var button = gameObject.GetComponent<CollectionItemButton>();
                button.Init(items[i]);
                itemButtons.Add(button);
            }
        }

        public void SetShown(bool shown)
        {
            header.gameObject.SetActive(shown);
            itemsContent.gameObject.SetActive(shown && expanded);
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
