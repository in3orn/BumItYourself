using DG.Tweening;
using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using Krk.Bum.View.Elements;
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

        [SerializeField]
        private FadeLabelView fadeLabelTemplate = null;


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
            foreach (var button in ItemButtons)
            {
                button.UpdateAppearance();
            }
        }

        public void ShowFadeLabel(StoreItemButton button, int count)
        {
            SpawnFadeLabel(button.GetComponent<RectTransform>(), button.Item.Reward * count);
        }

        private void SpawnFadeLabel(RectTransform parent, float value)
        {
            var gameObject = Instantiate(fadeLabelTemplate, parent);
            var fadeLabel = gameObject.GetComponent<FadeLabelView>();
            fadeLabel.Show(parent, Mathf.RoundToInt(value));
            DOVirtual.DelayedCall(5f, () => Destroy(fadeLabel.gameObject));  //TODO return to pool :)
        }
    }
}
