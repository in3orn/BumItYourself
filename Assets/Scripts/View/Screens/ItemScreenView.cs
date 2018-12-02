using Krk.Bum.Model;
using Krk.Bum.View.Elements;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class ItemScreenView : ScreenView
    {
        public Button BackButton;

        public Button CreateButton;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private TextMeshProUGUI itemName = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;

        [SerializeField]
        private RectTransform requiredPartsContent = null;

        [SerializeField]
        private RequiredPartRow requiredPartRow = null;


        private readonly List<RequiredPartRow> rows;


        public ItemScreenView()
        {
            rows = new List<RequiredPartRow>();
        }


        public void Init(ItemData item, RequiredPartData[] parts, bool canCreate)
        {
            itemName.text = item.TotalCount > 0 ? item.Name : "???";
            
            CreateButton.interactable = canCreate;
            if (item.TotalCount > 0)
            {
                InitImage(item.Image);
            }
            Init(parts);
        }

        private void InitImage(ImageData data) //TODO make some util method??
        {
            image.sprite = data.Image;
            image.color = data.Color;
            image.rectTransform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
            image.SetNativeSize();
        }

        private void Init(RequiredPartData[] items)
        {
            var size = Mathf.Min(items.Length, rows.Count);

            DisableItems(size);
            UpdateItems(items, size);
            CreateItems(items, size);
        }

        private void CreateItems(RequiredPartData[] items, int size)
        {
            for (int i = size; i < items.Length; i++)
            {
                var gameObject = Instantiate(requiredPartRow, requiredPartsContent);
                var row = gameObject.GetComponent<RequiredPartRow>();
                row.Init(items[i]);
                rows.Add(row);
            }
        }

        private void UpdateItems(RequiredPartData[] items, int size)
        {
            for (int i = 0; i < size; i++)
            {
                rows[i].gameObject.SetActive(true);
                rows[i].Init(items[i]);
            }
        }

        private void DisableItems(int size)
        {
            for (int i = size; i < rows.Count; i++)
            {
                rows[i].gameObject.SetActive(false);
            }
        }
    }
}
