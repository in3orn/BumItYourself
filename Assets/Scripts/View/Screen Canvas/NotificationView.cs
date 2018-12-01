using System;
using DG.Tweening;
using UnityEngine;

namespace Krk.Bum.View.Screen_Canvas
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mainTransform = null;

        [SerializeField]
        private RectTransform iconTransform = null;


        public bool Shown { get; private set; }


        private void Awake()
        {
            Deactivate();
            mainTransform.localScale = Vector3.zero;
        }

        public void Init()
        {
            Shown = true;
            Activate();
            mainTransform.localScale = Vector3.one;
        }

        public void Show()
        {
            Shown = true;
            Activate();
            mainTransform.DOScale(1f, .25f).SetEase(Ease.OutBounce);
        }

        public void Hide()
        {
            Shown = false;

            var sequence = DOTween.Sequence();

            sequence.Append(mainTransform.DOScale(0f, .25f));
            sequence.AppendCallback(Deactivate);

            sequence.Play();
        }

        private void Activate()
        {
            mainTransform.gameObject.SetActive(true);
        }

        private void Deactivate()
        {
            mainTransform.gameObject.SetActive(false);
        }
    }
}
