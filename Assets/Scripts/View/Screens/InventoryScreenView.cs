using System.Collections.Generic;
using Krk.Bum.Model;
using Krk.Bum.View.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class InventoryScreenView : ScreenView
    {
        public Button BackButton;

        [SerializeField]
        private CollectionView collectionView = null;

        [SerializeField]
        private RectTransform collectionsContent = null;


        public List<CollectionView> CollectionViews { get; private set; }


        public InventoryScreenView()
        {
            CollectionViews = new List<CollectionView>();
        }


        public void Init(CollectionData[] collections)
        {
            CollectionViews.Clear();

            foreach (var collection in collections)
            {
                var gameObject = Instantiate(this.collectionView, collectionsContent);
                var collectionView = gameObject.GetComponent<CollectionView>();
                collectionView.Init(collection);
                CollectionViews.Add(collectionView);
            }
        }
    }
}
