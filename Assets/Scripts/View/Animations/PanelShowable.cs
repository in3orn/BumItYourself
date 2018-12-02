using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Animations
{
    public class PanelShowable : Showable
    {
        [SerializeField]
        private Image background = null;

        [SerializeField]
        private RectTransform panel = null;


        private Color defaultBackgroundColor;

        private Vector2 defaultPanelPosition;

        private Vector2 targetPanelPosition;


        public void Awake()
        {
            defaultBackgroundColor = background.color;
            defaultPanelPosition = panel.anchoredPosition;
            targetPanelPosition = defaultPanelPosition;
            targetPanelPosition.y -= 2 * panel.rect.size.y;

            panel.anchoredPosition = targetPanelPosition;
            background.color = Color.clear;
        }

        public override void Show()
        {
            Activate();

            var sequence = DOTween.Sequence();

            sequence.Join(panel.DOAnchorPos(defaultPanelPosition, 0.5f).SetEase(Ease.OutQuad));
            sequence.Join(background.DOColor(defaultBackgroundColor, sequence.Duration()));

            sequence.Play();
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence.Join(panel.DOAnchorPos(targetPanelPosition, 0.5f).SetEase(Ease.InQuad));
            sequence.Join(background.DOColor(Color.clear, sequence.Duration()));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }
    }
}