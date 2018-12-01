using Krk.Bum.Game.Items;
using UnityEngine;

namespace Krk.Bum.Game.Actors
{
    public class PlayerController
    {
        private readonly PlayerConfig config;


        private Vector2 targetPosition;

        private IStreetItemController targetItem;


        public Vector2 TargetPosition
        {
            get { return targetPosition; }
            set
            {
                if(targetPosition != value)
                {
                    targetPosition = value;
                    targetItem = null;
                }
            }
        }

        public Vector2 Position { get; private set; }

        public IStreetItemController TargetItem
        {
            get { return targetItem; }
            set
            {
                if (targetItem != value)
                {
                    targetItem = value;
                    if (targetItem != null)
                    {
                        targetPosition = targetItem.Position;
                    }
                }
            }
        }


        public PlayerController(PlayerConfig config)
        {
            this.config = config;
        }

        public void Update(float deltaTime)
        {
            if (TargetItem != null && IsInRange(TargetItem.Position))
            {
                TargetItem.Use();
                TargetPosition = Position;
            }
            else
            {
                TargetPosition = new Vector2(TargetPosition.x, config.WalkRange.Clamp(TargetPosition.y));
                var diff = TargetPosition - Position;
                if (diff.magnitude > config.MinTargetDistance)
                {
                    Position += diff.normalized * config.WalkSpeed * deltaTime;
                }
            }
        }

        public bool IsInRange(Vector2 position)
        {
            var diff = position - Position;
            return diff.magnitude <= config.ReachRange;
        }
    }
}
