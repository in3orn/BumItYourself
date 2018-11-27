using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Core;
using UnityEngine;

namespace Krk.Bum.Game.Context
{
    public class GameContext : MonoBehaviour
    {
        private GameStateController gameStateController;

        public GameStateController GameStateController
        {
            get { return gameStateController ?? (gameStateController = new GameStateController()); }
        }


        public PlayerConfig PlayerConfig;

        private PlayerController playerController;

        public PlayerController PlayerController
        {
            get
            {
                return playerController ?? (playerController = new PlayerController(PlayerConfig));
            }
        }
    }
}
