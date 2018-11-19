using System;
using Krk.Bum.Game.Context;
using Krk.Bum.Game.Items;
using Krk.Bum.Model;
using UnityEngine;

namespace Krk.Bum.View.Street
{
    public class TrashMediator : MonoBehaviour
    {
        [SerializeField]
        private TrashView view = null;

        [SerializeField]
        private GameContext gameContext = null;


        private TrashController trashController;


        private void Awake()
        {
            trashController = gameContext.TrashController;
        }

        private void OnEnable()
        {
            view.OnClicked += HandleTrashClicked;

            trashController.OnHit += HandleTrashHit;
            trashController.OnEmptyHit += HandleTrashEmptyHit;
        }

        private void OnDisable()
        {
            if (view != null)
            {
                view.OnClicked -= HandleTrashClicked;
            }
        }

        private void HandleTrashClicked()
        {
            trashController.Hit();
        }

        private void HandleTrashEmptyHit(TrashData data)
        {
            view.HitEmpty(data);
        }

        private void HandleTrashHit(TrashData trashData, PartData partData)
        {
            view.Hit(trashData, partData);
        }
    }
}
