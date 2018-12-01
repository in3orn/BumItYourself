using Krk.Bum.Game.Context;
using Krk.Bum.Game.Core;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Screens
{
    public class SummaryScreenMediator : ScreenMediator
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private SummaryScreenView screenView = null;


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

            screenView.CollectButton.onClick.AddListener(HandleCollectButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (screenView != null)
            {
                screenView.CollectButton.onClick.RemoveListener(HandleCollectButtonClicked);
            }
        }

        private void HandleCollectButtonClicked()
        {
            CollectLoot();
            viewStateController.SetState(Model.ViewStateEnum.Street);
        }

        private void CollectLoot()
        {
            foreach (var loot in gameStateController.Loot)
            {
                var part = modelController.GetPart(loot.Id);
                modelController.CollectPart(part, part.Count);
            }
            gameStateController.ClearLoot();
        }


        protected override void SetShown(bool shown)
        {
            if (shown)
            {
                screenView.Init(gameStateController.Loot);
            }
            base.SetShown(shown);
        }
    }
}
