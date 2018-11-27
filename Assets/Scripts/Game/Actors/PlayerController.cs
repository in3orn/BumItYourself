using Krk.Bum.Game.Items;
using UnityEngine;

namespace Krk.Bum.Game.Actors
{
    public class PlayerController
    {
        private readonly PlayerConfig config;


        private Vector2 targetPosition;

        private TrashController targetTrash;


        public Vector2 TargetPosition
        {
            get { return targetPosition; }
            set
            {
                if(targetPosition != value)
                {
                    targetPosition = value;
                    targetTrash = null;
                }
            }
        }

        public Vector2 Position { get; private set; }

        public TrashController TargetTrash
        {
            get { return targetTrash; }
            set
            {
                if (targetTrash != value)
                {
                    targetTrash = value;
                    if (targetTrash != null)
                    {
                        targetPosition = targetTrash.Position;
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
            if (TargetTrash != null && IsInRange(TargetTrash.Position))
            {
                TargetTrash.Hit();
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
