using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class StaticDepthRenderer : MonoBehaviour
    {
        [SerializeField]
        private DepthRendererConfig config = null;

        [SerializeField]
        private SpriteRenderer[] depthRenderers = null;


        private void Start()
        {
            UpdateDepth();
        }

        protected void UpdateDepth()
        {
            foreach (var renderer in depthRenderers)
            {
                renderer.sortingOrder = 10000 + config.DepthOffset +
                     10 * Mathf.RoundToInt(100f * -transform.position.y);
            }
        }
    }
}
