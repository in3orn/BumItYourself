using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class ZAxisView : MonoBehaviour
    {
        private void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        }
    }
}
