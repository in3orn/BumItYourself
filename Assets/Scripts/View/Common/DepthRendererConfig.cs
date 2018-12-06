using UnityEngine;

namespace Krk.Bum.View.Common
{
    [CreateAssetMenu(menuName = "Krk/View/Common/Depth Renderer")]
    public class DepthRendererConfig : ScriptableObject
    {
        public int DepthOffset;
    }
}
