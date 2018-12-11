using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using Krk.Bum.View.Context;
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
            if (config.TestTemplates != null && config.TestTemplates.Length > 0)
            {
                var index = Random.Range(0, config.TestTemplates.Length);
                return config.TestTemplates[index];
            }
            else
            {
                var index = Random.Range(0, config.Templates.Length);
                return config.Templates[index];
            }
        }
    }
}
