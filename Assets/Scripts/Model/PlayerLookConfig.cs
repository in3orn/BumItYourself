﻿using UnityEngine;

namespace Krk.Bum.Model
{
    [CreateAssetMenu(menuName = "Krk/Model/Player Look")]
    public class PlayerLookConfig : ScriptableObject
    {
        public PlayerItemConfig[] Bodies;
        public PlayerItemConfig[] Bags;
        public PlayerItemConfig[] Sticks;
        public PlayerItemConfig[] Glasses;
        public PlayerItemConfig[] Beards;
    }
}
