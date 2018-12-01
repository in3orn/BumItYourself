using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class CountDisplay : MonoBehaviour
    {
        [SerializeField]
        private CountDisplayConfig config = null;

        [SerializeField]
        private RectTransform mainTransform = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;


        public bool Shown { get; private set; }


        private void Awake()
        {
            Shown = false;
            mainTransform.gameObject.SetActive(false);
        }

        public void Init(int value)
        {
            Shown = true;
            mainTransform.gameObject.SetActive(true);
            countLabel.text = "" + value;

            mainTransform.DOAnchorPosY(-mainTransform.anchoredPosition.y, config.ShowDuration).
                From().SetEase(Ease.OutBounce).SetDelay(config.ShowDelay);
        }

        public void Show(int value)
        {
            Shown = true;
            mainTransform.gameObject.SetActive(true);
            countLabel.text = "" + value;

            mainTransform.DOAnchorPosY(-mainTransform.anchoredPosition.y, config.ShowDuration).
                From().SetEase(Ease.OutBounce);
        }

        public void IncreaseValue(int value)
        {
            countLabel.text = "" + value;

            var sequence = DOTween.Sequence();

            sequence.Append(mainTransform.DOScale(1.2f, config.IncreaseDuration / 2f));
            sequence.Append(mainTransform.DOScale(1f, config.IncreaseDuration / 2f));
        }

        public void DecreaseValue(int value)
        {
            countLabel.text = "" + value;

            var sequence = DOTween.Sequence();

            sequence.Append(mainTransform.DOScale(.8f, config.DecreaseDuration / 2f));
            sequence.Append(mainTransform.DOScale(1f, config.DecreaseDuration / 2f));
        }
    }
}
