using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Core;
using Krk.Bum.Game.Items;
using Krk.Bum.Model.Context;
using UnityEngine;

namespace Krk.Bum.Game.Context
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;


        private GameStateController gameStateController;

        public GameStateController GameStateController
        {
            get { return gameStateController ?? (gameStateController = new GameStateController()); }
        }


        public TrashConfig TrashConfig;

        private TrashController trashController;

        public TrashController TrashController
        {
            get
            {
                return trashController ?? (trashController =
                  new TrashController(modelContext.ModelController, TrashConfig));
            }
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
