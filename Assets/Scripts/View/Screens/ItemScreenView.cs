using DG.Tweening;
using Krk.Bum.Model;
using Krk.Bum.View.Animations;
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
        private RectTransform itemFadeContent = null;

        [SerializeField]
        private Image itemFade = null;

        [SerializeField]
        private TextMeshProUGUI itemFadeText = null;

        [SerializeField]
        private CanvasGroup canvasGroup = null;

        [SerializeField]
        private FadeLabelView fadeLabelTemplate = null;

        [SerializeField]
        private ParticleSystem[] particleSystems = null;


        private readonly List<RequiredPartRow> rows;


        private Sprite defaultItemSprite;
        private Color defaultItemColor;

        private FadeColorData itemFadeColors;
        private PunchAnimationData commonScaleData;
        private PunchAnimationData commonRotationData;


        private Sequence updateFadeSequence;


        public ItemScreenView()
        {
            rows = new List<RequiredPartRow>();

            itemFadeColors = new FadeColorData();
            commonScaleData = new PunchAnimationData() { StartValue = Vector3.one };
            commonRotationData = new PunchAnimationData();
        }


        private void Awake()
        {
            defaultItemSprite = itemImage.sprite;
            defaultItemColor = itemImage.color;

            itemFadeColors.TargetColor = itemFade.color;
            itemFade.color = itemFadeColors.ClearColor;
            itemFade.gameObject.SetActive(false);

            itemFadeText.rectTransform.localScale = Vector3.zero;
            itemFadeText.gameObject.SetActive(false);

            updateFadeSequence = itemFade.DOFade(config.UpdateFade, itemFadeColors);
            updateFadeSequence.SetAutoKill(false);

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
                InitItem(item, parts, canCreate, PrevItemButton.interactable, NextItemButton.interactable);
                
                updateFadeSequence.Restart();

                itemImage.rectTransform.DOPunchScale(config.ItemScale, commonScaleData);
                itemImage.rectTransform.DOPunchRotation(config.ItemRoatation, commonRotationData);

                itemCount.rectTransform.DOPunchScale(config.CountScale, commonScaleData);
                itemCount.rectTransform.DOPunchRotation(config.CountRotation, commonRotationData);

                itemBackground.rectTransform.DOPunchScale(config.BackScale, commonScaleData);
                itemBackground.rectTransform.DOPunchRotation(config.BackRotation, commonRotationData);

                SpawnFadeLabel();
                SpawnParticles();
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
                sequence.Join(itemFade.DOColor(itemFadeColors.TargetColor,
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
                sequence.Join(itemFade.DOColor(itemFadeColors.ClearColor, config.FirstBackHideDuration));
                sequence.Join(itemFadeText.rectTransform.DOScale(
                    Vector2.zero, config.FirstBackHideDuration).SetEase(Ease.InOutElastic));

                sequence.AppendCallback(() => itemFade.gameObject.SetActive(false));
                sequence.AppendCallback(() => itemFadeText.gameObject.SetActive(false));

                sequence.Play();
            }
        }

        private void SpawnFadeLabel()
        {
            var gameObject = Instantiate(fadeLabelTemplate, itemFadeContent);
            var fadeLabel = gameObject.GetComponent<FadeLabelView>();
            fadeLabel.Show(itemImage.rectTransform);
            DOVirtual.DelayedCall(5f, () => Destroy(fadeLabel.gameObject));  //TODO return to pool :)
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
