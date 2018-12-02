using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Animations
{
    public class PanelShowable : Showable
    {
        [SerializeField]
        private PanelShowableConfig config = null;

        [SerializeField]
        private Image background = null;

        [SerializeField]
        private RectTransform panel = null;


        private Color defaultBackgroundColor;

        private Vector2 defaultPanelPosition;

        private Vector2 hiddenPanelPosition;


        public void Awake()
        {
            defaultBackgroundColor = background.color;
            defaultPanelPosition = panel.anchoredPosition;
            hiddenPanelPosition = defaultPanelPosition;
            hiddenPanelPosition -= 2 * config.ShowDirection * panel.rect.size;

            panel.anchoredPosition = hiddenPanelPosition;
            background.color = Color.clear;
        }

        public override void Show()
        {
            Activate();

            var sequence = DOTween.Sequence();

            sequence.Join(panel.DOAnchorPos(defaultPanelPosition, config.ShowDuration).SetEase(Ease.OutQuad));
            sequence.Join(background.DOColor(defaultBackgroundColor, sequence.Duration()));

            sequence.Play();
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence.Join(panel.DOAnchorPos(hiddenPanelPosition, config.HideDuration).SetEase(Ease.InQuad));
            sequence.Join(background.DOColor(Color.clear, sequence.Duration()));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }
    }
}