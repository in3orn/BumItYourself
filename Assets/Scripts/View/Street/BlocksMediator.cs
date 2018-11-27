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
        private BlockView[] templates = null;

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

        protected virtual BlocksController GetBlocksController()
        {
            return viewContext.BlocksController;
        }

        public void Update()
        {
            blocksController.UpdatePosition(playerController.Position.x);

            while (blocksController.ShouldSpawnLeftBlock())
            {
                var index = Random.Range(0, templates.Length);
                var template = templates[index];
                var gameObject = Instantiate(template, parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnLeftBlock(view);
                view.SetPosition(data.Center);
            }

            while (blocksController.ShouldSpawnRightBlock())
            {
                var index = Random.Range(0, templates.Length);
                var template = templates[index];
                var gameObject = Instantiate(template, parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnRightBlock(view);
                view.SetPosition(data.Center);
            }
        }
    }
}
