using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Animations
{
    public class SceneSwitchShowable : Showable
    {
        [SerializeField]
        private SceneSwitchShowableConfig config = null;

        [SerializeField]
        private Image foreground = null;

        [SerializeField]
        private GameObject scene = null;


        private Color defaultForegroundColor;


        public void Awake()
        {
            defaultForegroundColor = foreground.color;

            foreground.color = Color.clear;
            foreground.gameObject.SetActive(true);

            scene.SetActive(false);
        }

        public override void Show()
        {
            Activate();

            var sequence = DOTween.Sequence();
            
            sequence.Append(foreground.DOColor(defaultForegroundColor, config.ShowDuration / 2f));
            sequence.AppendCallback(() => scene.SetActive(true));
            sequence.Append(foreground.DOColor(Color.clear, config.ShowDuration / 2f));

            sequence.Play();
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(foreground.DOColor(defaultForegroundColor, config.ShowDuration / 2f));
            sequence.AppendCallback(() => scene.SetActive(false));
            sequence.Append(foreground.DOColor(Color.clear, config.ShowDuration / 2f));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }
    }
}
