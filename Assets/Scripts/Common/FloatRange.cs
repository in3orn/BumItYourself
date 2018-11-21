using System;
using UnityEngine;

namespace Krk.Bum.Common
{
    [Serializable]
    public class FloatRange
    {
        [SerializeField]
        private float min = 0f;

        [SerializeField]
        private float max = 0f;


        public float Min { get { return min; } }
        public float Max { get { return max; } }


        public float Clamp(float value)
        {
            return Mathf.Clamp(value, Min, Max);
        }
    }
}
