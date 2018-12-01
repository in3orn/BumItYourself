using DG.Tweening;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class StreetButtonView : MonoBehaviour
    {
        [SerializeField]
        private StreetButtonConfig config = null;

        [SerializeField]
        private RectTransform button = null;


        public bool Shown { get; private set; }


        private void Awake()
        {
            button.gameObject.SetActive(false);
            button.localScale = Vector3.zero;
        }

        public void Init()
        {
            DOVirtual.DelayedCall(config.InitDelay, Show);
        }

        public void Show()
        {
            Shown = true;
            button.gameObject.SetActive(true);
            button.DOScale(1f, config.ShowDuration).SetEase(Ease.OutBounce);
        }
    }
}
