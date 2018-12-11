﻿using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.Game.Items
{
    public class SellerController : IStreetItemController
    {
        public UnityAction OnStoreOpened;


        public Vector2 Position { get; set; }


        public void Use()
        {
            if (OnStoreOpened != null) OnStoreOpened();
        }
    }
}
