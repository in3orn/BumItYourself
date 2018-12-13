using Krk.Bum.Game.Context;
using Krk.Bum.Game.Core;
using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class GameScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private GameScreenView screenView = null;


        private ModelController modelController;

        private GameStateController gameStateController;


        protected override void Awake()
        {
            base.Awake();
            modelController = modelContext.ModelController;
            gameStateController = gameContext.GameStateController;
        }

        protected override ScreenView GetScreenView()
        {
            return screenView;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            screenView.TestButton.onClick.AddListener(HandleTestButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.TestButton.onClick.RemoveListener(HandleTestButtonClicked);
            }
        }

        private void HandleTestButtonClicked()
        {
            GetRandomLoot();
            viewStateController.SetState(Model.ViewStateEnum.Seller);
        }

        private void GetRandomLoot() //TODO test one
        {
            var size = Random.Range(1, 5);
            for (int i = 0; i < size; i++)
            {
                var parts = modelController.GetAllParts();
                var index = Random.Range(0, parts.Length);
                var count = Random.Range(1, 6);

                var part = parts[index];
                var loot = new PartData
                {
                    Id = part.Id,
                    Name = part.Name,
                    Image = part.Image,
                    Count = count
                };
                gameStateController.AddLoot(loot);
            }
        }
    }
}
