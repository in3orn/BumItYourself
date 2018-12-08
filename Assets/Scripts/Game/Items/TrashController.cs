using Krk.Bum.Model;
using Krk.Bum.Model.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.Game.Items
{
    public class TrashController : IStreetItemController
    {
        public UnityAction<TrashController, PartData> OnHit;
        public UnityAction<TrashController> OnEmptyHit;


        private readonly ModelController modelController;


        public TrashData State { get; private set; }

        public Vector2 Position { get; set; }


        public TrashController(ModelController modelController, TrashConfig config)
        {
            this.modelController = modelController;

            State = new TrashData
            {
                Capacity = config.Capacity,
                ItemsCount = config.Capacity
            };
        }

        public void Use()
        {
            if (!modelController.IsAnyCollectionUnlocked())
            {
                var collection = modelController.GetAllCollections()[0];
                var part = new PartData()
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    Image = collection.Image,
                    IsCollection = true
                };

                State.ItemsCount = 0;
                OnHit?.Invoke(this, part);

                return;
            }

            if (State.ItemsCount > 0)
            {
                State.ItemsCount--;
                var part = CollectRandomPart();
                OnHit?.Invoke(this, part);
            }
            else
            {
                OnEmptyHit?.Invoke(this);
            }
        }

        private PartData CollectRandomPart()
        {
            var parts = modelController.GetAllParts();

            var count = 1;
            var index = 0;
            PartData part = null;

            while (true)
            {
                index = Random.Range(0, parts.Length);
                part = parts[index];

                if (modelController.CanCollectPart(part))
                {
                    modelController.CollectPart(part, count);
                    break;
                }
            }

            return new PartData
            {
                Id = part.Id,
                Name = part.Name,
                Image = part.Image,
                Count = count
            };
        }
    }
}
