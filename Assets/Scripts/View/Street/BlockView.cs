using System;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class BlockView : MonoBehaviour
    {
        public float Width
        {
            get { return 10f; }
        }

        public void SetX(float centerX)
        {
            transform.position = new Vector3(centerX, transform.position.y, transform.position.z);
        }
    }
}
