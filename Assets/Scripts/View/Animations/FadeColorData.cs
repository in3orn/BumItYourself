using System;
using UnityEngine;

namespace Krk.Bum.View.Animations
{
    [Serializable]
    public class FadeColorData
    {
        private Color targetColor;

        public Color TargetColor
        {
            get { return targetColor; }
            set
            {
                if (targetColor != value)
                {
                    targetColor = value;
                    clearColor = value;
                    clearColor.a = 0f;
                }
            }
        }

        private Color clearColor;

        public Color ClearColor { get { return clearColor; } }
    }
}
