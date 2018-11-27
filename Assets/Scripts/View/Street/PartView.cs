using DG.Tweening;
using Krk.Bum.Model;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class PartView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer mainRenderer;

        public void Show(PartData partData)
        {
            mainRenderer.sprite = partData.Image.Image;
            mainRenderer.color = partData.Image.Color;

            var sequence = DOTween.Sequence();

            sequence.Append(mainRenderer.transform.DOMoveY(5f, 1f));
            sequence.Join(mainRenderer.DOColor(Color.clear, 1f));
            sequence.AppendCallback(() => { Destroy(gameObject); });
        }
    }
}
