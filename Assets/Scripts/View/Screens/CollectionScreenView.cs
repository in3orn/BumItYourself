using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class CollectionScreenView : ScreenView
    {
        public Button BackButton;

        [SerializeField]
        private ItemButton itemButton = null;

        [SerializeField]
        private RectTransform itemsContent = null;


        private readonly List<ItemButton> itemButtons;

        public List<ItemButton> ItemButtons { get { return itemButtons; } }


        public CollectionScreenView()
        {
            itemButtons = new List<ItemButton>();
        }

        public void Init(ItemData[] items)
        {
            var size = Mathf.Min(items.Length, itemButtons.Count);

            DisableItems(size);
            UpdateItems(items, size);
            CreateItems(items, size);
        }

        private void CreateItems(ItemData[] items, int size)
        {
            for (int i = size; i < items.Length; i++)
            {
                var gameObject = Instantiate(itemButton, itemsContent);
                var button = gameObject.GetComponent<ItemButton>();
                button.Init(items[i]);
                itemButtons.Add(button);
            }
        }

        private void UpdateItems(ItemData[] items, int size)
        {
            for (int i = 0; i < size; i++)
            {
                itemButtons[i].gameObject.SetActive(true);
                itemButtons[i].Init(items[i]);
            }
        }

        private void DisableItems(int size)
        {
            for (int i = size; i < itemButtons.Count; i++)
            {
                itemButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
