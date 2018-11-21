using UnityEngine;

namespace Krk.Bum.Game.Actors
{
    public class PlayerController
    {
        private readonly PlayerConfig config;


        public Vector2 TargetPosition { get; set; }
        
        public Vector2 Position { get; private set; }


        public PlayerController(PlayerConfig config)
        {
            this.config = config;
        }

        public void UpdatePosition(float deltaTime)
        {
            var diff = TargetPosition - Position;
            if(diff.magnitude > config.MinTargetDistance)
            {
                Position += diff.normalized * config.WalkSpeed * deltaTime;
            }
        }
    }
}
