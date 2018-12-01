using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using Krk.Bum.Game.Items;
using Krk.Bum.View.Street;
using UnityEngine;

namespace Krk.Bum.View.Inputs
{
    public class InputMediator : MonoBehaviour
    {
        [SerializeField]
        private InputController inputController = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private TrashMediator trashMediator = null;

        [SerializeField]
        private StoreMediator storeMediator = null;


        private PlayerController playerController;


        private void Awake()
        {
            playerController = gameContext.PlayerController;
        }

        private void OnEnable()
        {
            inputController.OnTapDown += HandleTapDown;
        }

        private void OnDisable()
        {
            if (inputController != null)
            {
                inputController.OnTapDown -= HandleTapDown;
            }
        }

        private void HandleTapDown(Vector2 position)
        {
            var trash = GetItem(position);
            if (trash != null)
            {
                playerController.TargetItem = trash;
            }
            else
            {
                playerController.TargetPosition = position;
            }
        }

        private IStreetItemController GetItem(Vector2 position)
        {
            var origin = new Vector3(position.x, position.y, -10f);
            var hit = Physics2D.Raycast(origin, Vector3.back, 100f);
            if (hit.collider != null)
            {
                var trashView = hit.collider.GetComponent<TrashView>();
                if (trashView != null)
                {
                    return trashMediator.GetControllerFor(trashView);
                }

                var storeView = hit.collider.GetComponent<StoreView>();
                if (storeView != null)
                {
                    return storeMediator.GetControllerFor(storeView);
                }

            }
            return null;
        }
    }
}
