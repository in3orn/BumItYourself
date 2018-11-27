using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer mainImage = null;
        
        public float Width
        {
            get { return mainImage.bounds.size.x; }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
