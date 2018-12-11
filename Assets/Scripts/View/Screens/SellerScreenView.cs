using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class SellerScreenView : ScreenView
    {
        public Button BackButton;


        [SerializeField]
        private SellerItemButton itemButton = null;

        [SerializeField]
        private RectTransform itemsContent = null;


        private readonly List<SellerItemButton> itemButtons;

        public List<SellerItemButton> ItemButtons { get { return itemButtons; } }


        public SellerScreenView()
        {
            itemButtons = new List<SellerItemButton>();
        }

        public void Init(PlayerItemData[] items)
        {
            var size = Mathf.Min(items.Length, itemButtons.Count);

            DisableItems(size);
            UpdateItems(items, size);
            CreateItems(items, size);
        }

        private void CreateItems(PlayerItemData[] items, int size)
        {
            for (int i = size; i < items.Length; i++)
            {
                var gameObject = Instantiate(itemButton, itemsContent);
                var button = gameObject.GetComponent<SellerItemButton>();
                button.Init(items[i]);
                itemButtons.Add(button);
            }
        }

        private void UpdateItems(PlayerItemData[] items, int size)
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
