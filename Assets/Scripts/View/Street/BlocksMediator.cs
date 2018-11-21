using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using Krk.Bum.View.Context;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class BlocksMediator : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private BlockView[] templates;

        [SerializeField]
        private ViewContext viewContext;

        [SerializeField]
        private GameContext gameContext;

        private BlocksController blocksController;

        private PlayerController playerController;


        private void Awake()
        {
            blocksController = viewContext.BlocksController;
            playerController = gameContext.PlayerController;
        }

        public void Update()
        {
            blocksController.UpdatePosition(playerController.Position.x);
            if(blocksController.ShouldSpawnLeftBlock())
            {
                var index = Random.Range(0, templates.Length);
                var template = templates[index];
                var gameObject = Instantiate(template, parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnLeftBlock(view);
                view.SetX(data.CenterX);
            }
            else if(blocksController.ShouldSpawnRightBlock())
            {
                var index = Random.Range(0, templates.Length);
                var template = templates[index];
                var gameObject = Instantiate(template, parent);
                var view = gameObject.GetComponent<BlockView>();
                var data = blocksController.SpawnRightBlock(view);
                view.SetX(data.CenterX);
            }
        }
    }
}
