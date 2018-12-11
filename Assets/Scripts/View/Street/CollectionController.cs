using Krk.Bum.Game.Items;
using Krk.Bum.Model.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.View.Street
{
    public class CollectionController : IStreetItemController
    {
        public UnityAction OnSpawned;
        public UnityAction OnUsed;


        private readonly ModelController modelController;


        public Vector2 Position { get; set; }


        public CollectionController(ModelController modelController)
        {
            this.modelController = modelController;
        }
        

        public void Use()
        {
            if (OnUsed != null) OnUsed();
        }

        public void Collect(string collectionId)
        {
            var collection = modelController.GetCollection(collectionId);
            modelController.UnlockCollection(collection);
        }

        public void Spawn()
        {
            if (OnSpawned != null) OnSpawned();
        }
    }
}