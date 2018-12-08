using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class IntroScreenView : ScreenView
    {
        public UnityAction OnIntroEnded;


        [SerializeField]
        private IntroScreenConfig config = null;

        [SerializeField]
        private Button exitButton = null;

        [SerializeField]
        private TextMeshProUGUI subtitle = null;


        private Color subtitleDefaultColor;


        private void Awake()
        {
            subtitleDefaultColor = subtitle.color;
            subtitle.color = Color.clear;
        }

        private void Start()
        {
            var sequence = DOTween.Sequence();

            foreach(var subtitleData in config.Subtitles)
            {
                sequence.AppendInterval(subtitleData.Delay);
                sequence.AppendCallback(() => SetSubtitle(subtitleData.Text));
                sequence.Append(subtitle.DOColor(subtitleDefaultColor, config.FadeInDuration));
                sequence.AppendInterval(subtitleData.Duration);
                sequence.Append(subtitle.DOColor(Color.clear, config.FadeOutDuration));
            }

            sequence.AppendCallback(Exit);

            sequence.Play();
        }

        private void OnEnable()
        {
            exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            if (exitButton != null)
            {
                exitButton.onClick.RemoveListener(Exit);
            }
        }

        private void Exit()
        {
            if (OnIntroEnded != null) OnIntroEnded();
        }

        private void SetSubtitle(string text)
        {
            subtitle.color = Color.clear;
            subtitle.text = text;
        }
    }
}
