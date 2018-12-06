using DG.Tweening;
using UnityEngine;

namespace Krk.Bum.View.Animations
{
    public class PanelShowable : Showable
    {
        [SerializeField]
        private PanelShowableConfig config = null;

        [SerializeField]
        private RectTransform panel = null;


        private Color defaultBackgroundColor;

        private Vector2 defaultPanelPosition;

        private Vector2 hiddenPanelPosition;


        public void Awake()
        {
            defaultPanelPosition = panel.anchoredPosition;
            hiddenPanelPosition = defaultPanelPosition;
            hiddenPanelPosition -= 2 * config.ShowDirection * panel.rect.size;
            panel.anchoredPosition = hiddenPanelPosition;

        }

        public override void Show()
        {
            Activate();

            var sequence = DOTween.Sequence();

            sequence.Append(panel.DOAnchorPos(defaultPanelPosition, config.ShowDuration).SetEase(Ease.OutQuad));

            sequence.Play();
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(panel.DOAnchorPos(hiddenPanelPosition, config.HideDuration).SetEase(Ease.InQuad));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }
    }
}