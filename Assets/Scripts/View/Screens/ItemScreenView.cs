using DG.Tweening;
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
        public Button PrevItemButton;
        public Button NextItemButton;


        [SerializeField]
        private TextMeshProUGUI itemName = null;

        [SerializeField]
        private RectTransform requiredPartsContent = null;

        [SerializeField]
        private RequiredPartRow requiredPartRow = null;

        [SerializeField]
        private Image itemImage = null;

        [SerializeField]
        private TextMeshProUGUI itemCount = null;

        [SerializeField]
        private CanvasGroup canvasGroup = null;


        private readonly List<RequiredPartRow> rows;


        private Sprite defaultItemSprite;
        private Color defaultItemColor;


        private void Awake()
        {
            defaultItemSprite = itemImage.sprite;
            defaultItemColor = itemImage.color;
        }

        public ItemScreenView()
        {
            rows = new List<RequiredPartRow>();
        }


        public void InitItem(ItemData item, RequiredPartData[] parts, bool canCreate, bool hasPrev, bool hasNext)
        {
            itemName.text = item.TotalCount > 0 ? item.Name : "???";

            CreateButton.interactable = canCreate;
            InitImage(item);
            Init(parts);

            PrevItemButton.interactable = hasPrev;
            NextItemButton.interactable = hasNext;
        }

        public void UpdateItem(ItemData item)
        {
            itemImage.rectTransform.DOPunchScale(Vector3.one, 0.25f);
            itemImage.rectTransform.DOPunchRotation(Vector3.right, 0.25f);

            itemCount.rectTransform.DOPunchScale(Vector3.one, 0.25f);
            itemCount.rectTransform.DOPunchRotation(Vector3.left, 0.25f);
        }

        public void SwitchItem(ItemData item, RequiredPartData[] parts, bool canCreate, bool hasPrev, bool hasNext)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(canvasGroup.DOFade(0f, .25f));
            sequence.AppendCallback(() => InitItem(item, parts, canCreate, hasPrev, hasNext));
            sequence.Append(canvasGroup.DOFade(1f, .25f));

            sequence.Play();
        }

        private void InitImage(ItemData item)
        {
            if (item.TotalCount > 0)
            {
                itemImage.sprite = item.Image.Image;
                itemImage.color = item.Image.Color;
                itemImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, item.Image.Rotation);
            }
            else
            {
                itemImage.sprite = defaultItemSprite;
                itemImage.color = defaultItemColor;
                itemImage.rectTransform.rotation = Quaternion.Euler(Vector3.zero);
            }

            itemCount.text = "" + item.Count;
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
