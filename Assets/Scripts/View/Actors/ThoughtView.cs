using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.View.Actors
{
    public class ThoughtView : MonoBehaviour
    {
        public UnityAction OnThoughtEnded;


        [SerializeField]
        private ThoughtViewConfig config = null;

        [SerializeField]
        private SpriteRenderer thoughtCloud = null;

        [SerializeField]
        private TextMeshPro thoughtText = null;


        private Color thoughtCloudDefaultColor;
        private Color thoughtTextDefaultColor;


        private void Awake()
        {
            thoughtCloudDefaultColor = thoughtCloud.color;
            thoughtCloud.color = Color.clear;

            thoughtTextDefaultColor = thoughtText.color;
            thoughtText.color = Color.clear;

            thoughtCloud.gameObject.SetActive(false);
        }

        public void Show(ThoughtData thinkData)
        {
            StartThought(thinkData);

            var sequence = DOTween.Sequence();

            sequence.SetDelay(thinkData.Delay);
            sequence.Append(thoughtCloud.DOColor(thoughtCloudDefaultColor, config.FadeInDuration));
            sequence.Join(thoughtText.DOColor(thoughtTextDefaultColor, config.FadeInDuration));
            sequence.AppendInterval(thinkData.Duration);
            sequence.Append(thoughtCloud.DOColor(Color.clear, config.FadeOutDuration));
            sequence.Join(thoughtText.DOColor(Color.clear, config.FadeOutDuration));
            sequence.AppendCallback(EndThought);

            sequence.Play();
        }

        private void StartThought(ThoughtData thinkData)
        {
            thoughtText.text = thinkData.Text;
            thoughtCloud.gameObject.SetActive(true);
        }

        private void EndThought()
        {
            thoughtCloud.gameObject.SetActive(false);
            if (OnThoughtEnded != null) OnThoughtEnded();
        }
    }
}
