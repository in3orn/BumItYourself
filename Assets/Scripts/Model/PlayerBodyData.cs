using System;
using UnityEngine;

namespace Krk.Bum.Model
{
    [Serializable]
    public class PlayerBodyData
    {
        public string Id;
        public string Name;
        public float Price;
        public Sprite Image;

        public bool Unlocked;
    }
}
