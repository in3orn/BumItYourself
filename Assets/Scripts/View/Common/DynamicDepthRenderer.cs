namespace Krk.Bum.View.Common
{
    public class DynamicDepthRenderer : StaticDepthRenderer
    {
        private void LateUpdate()
        {
            UpdateDepth();
        }
    }
}
