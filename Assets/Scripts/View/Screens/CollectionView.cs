using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class CollectionView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameLabel = null;

        [SerializeField]
        private ItemButton itemButton = null;

        [SerializeField]
        private RectTransform header = null;

        [SerializeField]
        private RectTransform itemsContent = null;


        private readonly List<ItemButton> itemButtons;

        public List<ItemButton> ItemButtons { get { return itemButtons; } }


        public CollectionView()
        {
            itemButtons = new List<ItemButton>();
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
                var button = gameObject.GetComponent<ItemButton>();
                button.Init(items[i]);
                itemButtons.Add(button);
            }
        }

        public void SetShown(bool shown)
        {
            header.gameObject.SetActive(shown);
            itemsContent.gameObject.SetActive(shown);
        }
    }
}
