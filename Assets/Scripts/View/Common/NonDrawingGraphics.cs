using UnityEngine.UI;

namespace Krk.Bum.View.Common
{
    public class NonDrawingGraphics : Graphic
    {
        public override void SetMaterialDirty() { return; }
        public override void SetVerticesDirty() { return; }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            return;
        }
    }
}
