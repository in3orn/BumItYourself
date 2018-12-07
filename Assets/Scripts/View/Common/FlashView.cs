using DG.Tweening;
using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class FlashView : MonoBehaviour
    {
        [SerializeField]
        private FlashConfig config = null;

        [SerializeField]
        private CanvasGroup flash = null;


        private void Awake()
        {
            flash.gameObject.SetActive(false);
            flash.alpha = 0f;
        }


        public void Flash()
        {
            flash.gameObject.SetActive(true);

            var sequence = DOTween.Sequence();

            sequence.Append(flash.DOFade(1f, config.FadeInDuration));
            sequence.Append(flash.DOFade(0f, config.FadeOutDuration));
            sequence.AppendCallback(() => flash.gameObject.SetActive(true));

            sequence.Play();
        }
    }
}
