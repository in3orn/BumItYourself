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
                if (modelController.IsAnyCollectionSpawned)
                {
                    OnEmptyHit?.Invoke(this);
                    return;
                }

                var collection = modelController.GetAllCollections()[0];
                modelController.IsAnyCollectionSpawned = true;

                var part = new PartData()
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    Image = collection.Image,
                    IsCollection = true
                };

                State.ItemsCount--;
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
            PartData spawned = null;

            var currentValue = 0f;
            var spawnValue = Random.value * GetSpawnValue();
            foreach(var part in modelController.GetAllParts())
            {
                if (modelController.CanCollectPart(part))
                {
                    currentValue += part.SpawnRatio;
                    if(spawnValue < currentValue)
                    {
                        spawned = part;
                        break;
                    }
                }
            }

            return new PartData
            {
                Id = spawned.Id,
                Name = spawned.Name,
                Image = spawned.Image,
                Count = 1
            };
        }

        private float GetSpawnValue()
        {
            var result = 0f;

            foreach (var part in modelController.GetAllParts())
            {
                if (modelController.CanCollectPart(part))
                {
                    result += part.SpawnRatio;
                }
            }

            return result;
        }
    }
}
