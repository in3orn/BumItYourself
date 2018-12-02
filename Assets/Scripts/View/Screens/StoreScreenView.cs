using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class StoreScreenView : ScreenView
    {
        public Button BackButton;
        public Button SellAllButton;


        [SerializeField]
        private StoreItemButton itemButton = null;

        [SerializeField]
        private RectTransform itemsContent = null;


        private readonly List<StoreItemButton> itemButtons;

        public List<StoreItemButton> ItemButtons { get { return itemButtons; } }


        public StoreScreenView()
        {
            itemButtons = new List<StoreItemButton>();
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
                var button = gameObject.GetComponent<StoreItemButton>();
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

        public void UpdateAppearance()
        {
            foreach(var button in ItemButtons)
            {
                button.UpdateAppearance();
            }
        }
    }
}
