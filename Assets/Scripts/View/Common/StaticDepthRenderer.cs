using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class StaticDepthRenderer : MonoBehaviour
    {
        [SerializeField]
        private DepthRendererConfig config;

        [SerializeField]
        private SpriteRenderer depthRenderer;


        private void Start()
        {
            UpdateDepth();
        }

        protected void UpdateDepth()
        {
            depthRenderer.sortingOrder = 10000 + config.DepthOffset +
                10 * Mathf.RoundToInt(100f * -transform.position.y);
        }
    }
}
