using System;
using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using UnityEngine;

namespace Krk.Bum.View.Inputs
{
    public class InputMediator : MonoBehaviour
    {
        [SerializeField]
        private InputController inputController = null;

        [SerializeField]
        private GameContext gameContext = null;


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
            if(inputController != null)
            {
                inputController.OnTapDown -= HandleTapDown;
            }
        }

        private void HandleTapDown(Vector2 position)
        {
            playerController.TargetPosition = position;
        }
    }
}
