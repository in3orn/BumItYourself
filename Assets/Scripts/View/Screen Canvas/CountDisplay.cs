using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class CountDisplay : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mainTransform = null;

        [SerializeField]
        private TextMeshProUGUI countLabel = null;


        public void InitValue(int value)
        {
            countLabel.text = "" + value;
        }

        public void IncreaseValue(int value)
        {
            countLabel.text = "" + value;

            var sequence = DOTween.Sequence();

            sequence.Append(mainTransform.DOScale(1.2f, .125f));
            sequence.Append(mainTransform.DOScale(1f, .125f));
        }

        public void DecreaseValue(int value)
        {
            countLabel.text = "" + value;

            var sequence = DOTween.Sequence();

            sequence.Append(mainTransform.DOScale(.8f, .125f));
            sequence.Append(mainTransform.DOScale(1f, .125f));
        }
    }
}
