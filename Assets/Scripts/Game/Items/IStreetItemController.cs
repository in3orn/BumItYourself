using UnityEngine;

namespace Krk.Bum.Game.Items
{
    public interface IStreetItemController
    {
        Vector2 Position { get; set; }

        void Use();
    }
}
