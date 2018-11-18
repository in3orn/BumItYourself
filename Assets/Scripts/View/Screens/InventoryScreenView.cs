﻿using System;
using System.Collections.Generic;
using Krk.Bum.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Krk.Bum.View.Buttons
{
    public class InventoryScreenView : ScreenView
    {
        public Button BackButton;

        [SerializeField]
        private CollectionButton collectionButton = null;

        [SerializeField]
        private RectTransform collectionsContent = null;
        

        public List<CollectionButton> CollectionButtons { get; private set; }
        

        public InventoryScreenView()
        {
            CollectionButtons = new List<CollectionButton>();
        }


        public void Init(CollectionData[] collections)
        {
            CollectionButtons.Clear();

            foreach (var collection in collections)
            {
                var gameObject = Instantiate(collectionButton, collectionsContent);
                var button = gameObject.GetComponent<CollectionButton>();
                button.Init(collection);
                CollectionButtons.Add(button);
            }
        }
    }
}
