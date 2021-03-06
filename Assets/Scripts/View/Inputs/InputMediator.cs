﻿using Krk.Bum.Game.Actors;
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

        [SerializeField]
        private SellerMediator sellerMediator = null;

        [SerializeField]
        private CollectionMediator collectionMediator = null;


        private PlayerController playerController;


        private void Awake()
        {
            playerController = gameContext.PlayerController;
        }

        private void OnEnable()
        {
            inputController.OnTapDown += HandleTapDown;
            inputController.OnHoldDown += HandleHoldDown;
        }

        private void OnDisable()
        {
            if (inputController != null)
            {
                inputController.OnTapDown -= HandleTapDown;
                inputController.OnHoldDown -= HandleHoldDown;
            }
        }

        private void HandleTapDown(Vector2 position)
        {
            var item = GetItem(position);
            if (item != null)
            {
                playerController.TargetItem = item;
                inputController.Release();
            }
            else
            {
                playerController.TargetPosition = position;
            }
        }

        private void HandleHoldDown(Vector2 position)
        {
            var item = GetItem(position);
            if (item != null)
            {
                playerController.TargetPosition = item.Position;
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

                var sellerView = hit.collider.GetComponent<SellerView>();
                if (sellerView != null)
                {
                    return sellerMediator.GetControllerFor(sellerView);
                }

                var partView = hit.collider.GetComponent<PartView>();
                if (partView != null)
                {
                    return collectionMediator.GetControllerFor(partView);
                }

            }
            return null;
        }
    }
}
