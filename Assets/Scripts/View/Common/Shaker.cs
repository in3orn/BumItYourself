using DG.Tweening;
using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField]
        private ShakerConfig config = null;


        public void Shake()
        {
            transform.DOShakePosition(config.Duration, config.Strength, config.Vibrato);
        }
    }
}
