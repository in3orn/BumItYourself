using DG.Tweening;
using Krk.Bum.Model;
using Krk.Bum.View.Elements;
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
        private ItemScreenConfig config = null;

        [SerializeField]
        private TextMeshProUGUI itemName = null;

        [SerializeField]
        private RectTransform requiredPartsContent = null;

        [SerializeField]
        private RequiredPartRow requiredPartRow = null;

        [SerializeField]
        private Image itemImage = null;

        [SerializeField]
        private Image itemBackground = null;

        [SerializeField]
        private TextMeshProUGUI itemCount = null;

        [SerializeField]
        private Image itemFade = null;

        [SerializeField]
        private TextMeshProUGUI itemFadeText = null;

        [SerializeField]
        private CanvasGroup canvasGroup = null;

        [SerializeField]
        private ParticleSystem[] particleSystems = null;


        private readonly List<RequiredPartRow> rows;


        private Sprite defaultItemSprite;
        private Color defaultItemColor;
        private Color defaultItemFadeColor;


        public ItemScreenView()
        {
            rows = new List<RequiredPartRow>();
        }


        private void Awake()
        {
            defaultItemSprite = itemImage.sprite;
            defaultItemColor = itemImage.color;

            defaultItemFadeColor = itemFade.color;
            itemFade.color = Color.clear;
            itemFade.gameObject.SetActive(false);

            itemFadeText.rectTransform.localScale = Vector3.zero;
            itemFadeText.gameObject.SetActive(false);

            InitParticles();
        }

        private void InitParticles()
        {
            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Stop();
            }
        }


        public void InitItem(ItemData item, RequiredPartData[] parts, bool canCreate, bool hasPrev, bool hasNext)
        {
            itemName.text = item.TotalCount > 0 ? item.Name : "???";
            itemFadeText.text = item.Name;

            CreateButton.interactable = canCreate;
            CreateButton.GetComponent<Image>().color =
                canCreate ? config.CreateButtonActive : config.CreateButtonLocked;

            InitImage(item);
            Init(parts);

            PrevItemButton.interactable = hasPrev;
            NextItemButton.interactable = hasNext;
        }

        public void UpdateItem(ItemData item, RequiredPartData[] parts, bool canCreate)
        {
            if (item.TotalCount > 1)
            {
                itemImage.rectTransform.localScale = Vector3.one;
                itemImage.rectTransform.rotation = Quaternion.Euler(Vector3.zero);

                SpawnParticles();
                InitItem(item, parts, canCreate, PrevItemButton.interactable, NextItemButton.interactable);

                itemImage.rectTransform.DOPunchScale(Vector3.one, 0.25f);
                itemImage.rectTransform.DOPunchRotation(Vector3.right, 0.25f);

                itemCount.rectTransform.DOPunchScale(Vector3.one, 0.25f);
                itemCount.rectTransform.DOPunchRotation(Vector3.left, 0.25f);
            }
            else
            {
                itemFade.gameObject.SetActive(true);

                itemImage.rectTransform.localScale = Vector3.one;
                itemImage.rectTransform.rotation = Quaternion.Euler(Vector3.zero);

                itemFadeText.rectTransform.localScale = Vector3.zero;
                itemFadeText.gameObject.SetActive(true);

                var sequence = DOTween.Sequence();

                sequence.Append(itemBackground.rectTransform.DOScale(config.FirstBackShowScale,
                    config.FirstBackShowDuration));
                sequence.Join(itemImage.rectTransform.DOScale(config.FirstItemShowScale,
                    config.FirstItemShowDuration).SetEase(Ease.OutQuad));
                sequence.Join(itemFade.DOColor(defaultItemFadeColor,
                    config.FirstBackShowDuration));

                sequence.AppendCallback(SpawnParticles);
                sequence.AppendCallback(() => InitItem(item, parts, canCreate,
                    PrevItemButton.interactable, NextItemButton.interactable));

                sequence.Append(itemBackground.rectTransform.DOPunchRotation(
                    Vector3.forward * config.FirstBackRotationStrength,
                    config.FirstBackActionDuration, config.FirstBackRotationVibrato));
                sequence.Join(itemImage.rectTransform.DOScale(
                    Vector2.one, config.FirstItemActionDuration).SetEase(Ease.OutElastic));
                sequence.Join(itemFadeText.rectTransform.DOScale(
                    Vector2.one, config.FirstBackActionDuration).SetEase(Ease.OutElastic));

                sequence.AppendInterval(config.FirstHideDelay);

                sequence.Append(itemBackground.rectTransform.DOScale(
                    Vector2.one, config.FirstBackHideDuration).SetEase(Ease.InOutElastic));
                sequence.Join(itemFade.DOColor(Color.clear, config.FirstBackHideDuration));
                sequence.Join(itemFadeText.rectTransform.DOScale(
                    Vector2.zero, config.FirstBackHideDuration).SetEase(Ease.InOutElastic));

                sequence.AppendCallback(() => itemFade.gameObject.SetActive(false));
                sequence.AppendCallback(() => itemFadeText.gameObject.SetActive(false));

                sequence.Play();
            }
        }

        public void SpawnParticles()
        {
            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Stop();
                particleSystem.Play();
            }
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
