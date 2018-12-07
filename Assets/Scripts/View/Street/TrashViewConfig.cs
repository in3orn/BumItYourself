using UnityEngine;

namespace Krk.Bum.View.Street
{
    [CreateAssetMenu(menuName = "Krk/View/Street/Trash View")]
    public class TrashViewConfig : ScriptableObject
    {
        [Header("Hit")]
        public Color HitColor;
        public float HitDuration;

        [Header("Empty Hit")]
        public Color EmptyHitColor;
        public float EmptyHitDuration;

        public Sprite TrashTopHitBack;
        public Sprite TrashTopHitFront;

        public Sprite[] DamagedImages;

        public float[] DamagedTopRotations;
        public Vector2[] DamagedTopPositions;
    }
}
