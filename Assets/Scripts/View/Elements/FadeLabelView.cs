using DG.Tweening;
using Krk.Bum.View.Animations;
using TMPro;
using UnityEngine;

namespace Krk.Bum.View.Elements
{
    public class FadeLabelView : MonoBehaviour
    {
        [SerializeField]
        private FadeLabelConfig config = null;

        [SerializeField]
        private TextMeshProUGUI label = null;


        private FadeColorData fadeColors;


        public FadeLabelView()
        {
            fadeColors = new FadeColorData();
        }

        private void Awake()
        {
            fadeColors.TargetColor = label.color;
        }

        public void Show(RectTransform rectTransform, int value = 1)
        {
            label.rectTransform.position = rectTransform.position;
            label.rectTransform.DOAnchorPos(config.Move);
            label.text = string.Format(config.Format, value);

            label.DOFade(config.Fade, fadeColors);
        }
    }
}
