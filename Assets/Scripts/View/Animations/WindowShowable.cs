using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Animations
{
    public class WindowShowable : Showable
    {
        [SerializeField]
        private Image background = null;

        [SerializeField]
        private RectTransform window = null;


        private Color defaultBackgroundColor;

        private Vector2 defaultWindowScale;
        

        public void Awake()
        {
            defaultBackgroundColor = background.color;
            defaultWindowScale = window.localScale;

            background.color = Color.clear;
            window.localScale = Vector2.zero;
        }

        public override void Show()
        {
            Activate();

            var sequence = DOTween.Sequence();

            sequence.Join(window.DOScale(defaultWindowScale, 0.5f).SetEase(Ease.OutBounce));
            sequence.Join(background.DOColor(defaultBackgroundColor, sequence.Duration()));

            sequence.Play();
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence.Join(window.DOScale(0f, 0.5f).SetEase(Ease.InBounce));
            sequence.Join(background.DOColor(Color.clear, sequence.Duration()));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }
    }
}
