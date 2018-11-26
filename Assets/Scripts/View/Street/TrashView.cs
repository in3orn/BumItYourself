using DG.Tweening;
using Krk.Bum.Game.Items;
using Krk.Bum.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.View.Street
{
    [RequireComponent(typeof(Collider2D))]
    public class TrashView : MonoBehaviour
    {
        public UnityAction OnClicked;


        [SerializeField]
        private TrashViewConfig config = null;

        [SerializeField]
        private SpriteRenderer trashBodyRenderer = null;

        [SerializeField]
        private SpriteRenderer trashTopRenderer = null;


        private Color trashDefaultColor;

        private int damage;

        private Vector3 temp;

        private void Awake()
        {
            trashDefaultColor = trashBodyRenderer.color;
            trashBodyRenderer.sprite = config.DamagedImages[damage];
        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke();
        }

        public void HitEmpty(TrashData data)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(trashBodyRenderer.DOColor(config.EmptyHitColor, config.EmptyHitDuration / 2f));
            sequence.Join(trashTopRenderer.DOColor(config.EmptyHitColor, config.EmptyHitDuration / 2f));
            sequence.Append(trashBodyRenderer.DOColor(trashDefaultColor, config.EmptyHitDuration / 2f));
            sequence.Join(trashTopRenderer.DOColor(trashDefaultColor, config.EmptyHitDuration / 2f));

            sequence.Play();
        }

        public void Hit(TrashData trashData, PartData partData)
        {
            damage++;

            if (damage < config.DamagedImages.Length)
            {
                trashBodyRenderer.sprite = config.DamagedImages[damage];
            }

            var sequence = DOTween.Sequence();

            sequence.Append(trashBodyRenderer.DOColor(config.EmptyHitColor, config.HitDuration / 2f));
            sequence.Join(trashTopRenderer.DOColor(config.EmptyHitColor, config.HitDuration / 2f));
            sequence.Append(trashBodyRenderer.DOColor(trashDefaultColor, config.HitDuration / 2f));
            sequence.Join(trashTopRenderer.DOColor(trashDefaultColor, config.HitDuration / 2f));

            if (damage < config.DamagedTopRotations.Length)
            {
                temp.x = 0f;
                temp.y = 0f;
                temp.z = config.DamagedTopRotations[damage];

                sequence.Insert(0f, trashTopRenderer.transform.DORotate(
                    temp, config.EmptyHitDuration).SetEase(Ease.InOutBounce));
            }

            if (damage < config.DamagedTopPositions.Length)
            {
                temp.x = config.DamagedTopPositions[damage].x;
                temp.y = config.DamagedTopPositions[damage].y;
                temp.z = trashTopRenderer.transform.localPosition.z;

                sequence.Insert(0f, trashTopRenderer.transform.DOLocalMove(
                    temp, config.EmptyHitDuration).SetEase(Ease.InBounce));
            }

            sequence.Play();
        }
    }
}
