using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using Krk.Bum.View.Context;
using System.Collections.Generic;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class BlocksMediator : MonoBehaviour
    {
        [SerializeField]
        private Transform parent = null;

        [SerializeField]
        private BlocksMediatorConfig config = null;

        [SerializeField]
        protected ViewContext viewContext = null;

        [SerializeField]
        protected GameContext gameContext = null;


        private BlocksController blocksController;

        private PlayerController playerController;


        private readonly List<BlockView> blocksToSpawn;


        public BlocksMediator()
        {
            blocksToSpawn = new List<BlockView>();
        }


        private void Awake()
        {
            blocksController = GetBlocksController();
            playerController = gameContext.PlayerController;
        }

        private void Start()
        {
            var gameObject = Instantiate(config.FirstTemplate, parent);
            var view = gameObject.GetComponent<BlockView>();
            var data = blocksController.SpawnFirstBlock(view);
            view.SetPosition(data.Center);
        }

        protected virtual BlocksController GetBlocksController()
        {
            return viewContext.BlocksController;
        }

        public void Update()
        {
            blocksController.UpdatePosition(playerController.Position.x);

            while (blocksController.ShouldSpawnLeftBlock())
            {
                var gameObject = Instantiate(GetRandomTemplate(), parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnLeftBlock(view);
                view.SetPosition(data.Center);
            }

            while (blocksController.ShouldSpawnRightBlock())
            {
                var gameObject = Instantiate(GetRandomTemplate(), parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnRightBlock(view);
                view.SetPosition(data.Center);
            }
        }

        private BlockView GetRandomTemplate()
        {
            if (blocksToSpawn.Count <= 0)
            {
                if (config.TestTemplates != null && config.TestTemplates.Length > 0)
                {
                    blocksToSpawn.AddRange(config.TestTemplates);
                }
                else
                {
                    blocksToSpawn.AddRange(config.Templates);
                }
            }

            var index = Random.Range(0, blocksToSpawn.Count);
            var result = blocksToSpawn[index];
            blocksToSpawn.RemoveAt(index);
            return result;
        }
    }
}
