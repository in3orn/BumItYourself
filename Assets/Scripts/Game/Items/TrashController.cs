using Krk.Bum.Model;
using Krk.Bum.Model.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Bum.Game.Items
{
    public class TrashController
    {
        public UnityAction<TrashData, PartData> OnHit;
        public UnityAction<TrashData> OnEmptyHit;


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

        public void Hit()
        {
            if (State.ItemsCount > 0)
            {
                State.ItemsCount--;
                var part = CollectRandomPart();
                OnHit?.Invoke(State, part);
            }
            else
            {
                OnEmptyHit?.Invoke(State);
            }
        }

        private PartData CollectRandomPart()
        {
            var parts = modelController.GetAllParts();
            var index = Random.Range(0, parts.Length);

            var part = parts[index];
            var count = 1;

            modelController.CollectPart(part.Id, count);

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
