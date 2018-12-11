using DG.Tweening;
using Krk.Bum.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.View.Street
{
    public class PartView : MonoBehaviour
    {
        public UnityAction<PartView> OnCollected;


        [SerializeField]
        private PartViewConfig config = null;

        [SerializeField]
        private SpriteRenderer mainRenderer = null;


        public RectTransform TargetTransform { get; set; }

        public PartData Data { get; private set; }


        public int DrawOrder
        {
            set { mainRenderer.sortingOrder = value; }
        }


        public void Spawn(PartData partData)
        {
            Data = partData;

            mainRenderer.sprite = partData.Image.Image;
            mainRenderer.color = partData.Image.Color;

            var sequence = DOTween.Sequence();

            var flyY = transform.position.y + config.FlyYRange.GetRandom();
            var layY = transform.position.y + config.LayYRange.GetRandom();
            var layX = transform.position.x + config.LayXRange.GetRandom();

            mainRenderer.transform.position = mainRenderer.transform.position + Vector3.up * config.StartYOffset;
            mainRenderer.transform.localScale = Vector3.zero;

            sequence.Append(mainRenderer.transform.DOMoveY(flyY, config.FlyUpDuration).SetEase(Ease.InQuad));
            sequence.Append(mainRenderer.transform.DOMoveY(layY, config.FlyDownDuration).SetEase(Ease.OutBounce));
            sequence.Insert(0f, mainRenderer.transform.DOMoveX(layX, config.FlyDuration));
            sequence.Insert(0f, mainRenderer.transform.DOScale(Vector3.one, config.FlyDuration));

            if (!partData.IsCollection)
            {
                sequence.AppendInterval(config.LayDownDuration);
                sequence.AppendCallback(Collect);
            }

            sequence.Play();
        }

        public void Collect()
        {
            StartCoroutine(Collect(mainRenderer.transform.position));
        }

        private IEnumerator Collect(Vector3 position)
        {
            var color = mainRenderer.color;
            var time = 0f;

            while (time < config.DragDuration)
            {
                var lerp = time / config.DragDuration;

                var targetPosition = Camera.main.ScreenToWorldPoint(TargetTransform.position);
                mainRenderer.transform.position = Vector3.Lerp(position, targetPosition, lerp);

                color.a = Mathf.Lerp(1f, 0f, lerp);
                mainRenderer.color = color;

                time += Time.deltaTime;
                yield return null;
            }

            if (OnCollected != null) OnCollected(this);

            Destroy(gameObject);
        }
    }
}
