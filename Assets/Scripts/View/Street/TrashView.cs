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
        private SpriteRenderer trashRenderer = null;


        private Color trashDefaultColor;


        private void Awake()
        {
            trashDefaultColor = trashRenderer.color;
        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke();
        }

        public void HitEmpty(TrashData data)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(trashRenderer.DOColor(config.EmptyHitColor, config.EmptyHitDuration / 2f));
            sequence.Append(trashRenderer.DOColor(trashDefaultColor, config.EmptyHitDuration / 2f));

            sequence.Play();
        }

        public void Hit(TrashData trashData, PartData partData)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(trashRenderer.DOColor(config.HitColor, config.HitDuration / 2f));
            sequence.Append(trashRenderer.DOColor(trashDefaultColor, config.HitDuration / 2f));

            sequence.Play();
        }
    }
}
