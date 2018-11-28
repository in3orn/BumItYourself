using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class ResourcesDisplay : MonoBehaviour
    {
        [SerializeField]
        private RectTransform resourcesTransform = null;

        [SerializeField]
        private TextMeshProUGUI resourcesValue = null;


        public void InitValue(int value)
        {
            resourcesValue.text = "" + value;
        }

        public void UpdateValue(int value)
        {
            resourcesValue.text = "" + value;

            var sequence = DOTween.Sequence();

            sequence.Append(resourcesTransform.DOScale(2f, .25f));
            sequence.Append(resourcesTransform.DOScale(1f, .25f));
        }
    }
}
