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

        public void SetX(float centerX)
        {
            transform.position = new Vector3(centerX, transform.position.y, transform.position.z);
        }
    }
}
