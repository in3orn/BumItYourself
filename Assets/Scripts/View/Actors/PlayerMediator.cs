using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using Krk.Bum.View.Common;
using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class PlayerMediator : MonoBehaviour
    {
        [SerializeField]
        private PlayerView playerView = null;

        [SerializeField]
        private GameContext gameContext = null;

        [SerializeField]
        private Shaker cameraShaker = null;

        [SerializeField]
        private FlashView hitFlash = null;


        private PlayerController playerController;

        private Vector3 playerPosition;


        private void Awake()
        {
            playerController = gameContext.PlayerController;
        }


        private void OnEnable()
        {
            playerController.OnHit += HandlePlayerHit;
        }

        private void OnDisable()
        {
            if (gameContext != null && playerView != null)
            {
                playerController.OnHit -= HandlePlayerHit;
            }
        }

        private void HandlePlayerHit(Vector2 target)
        {
            playerView.Hit(target);
            hitFlash.Flash();
            cameraShaker.Shake();
        }


        private void Update()
        {
            playerController.Update(Time.deltaTime);

            if ((Vector2)playerView.transform.position != playerController.Position)
            {
                playerPosition.x = playerController.Position.x;
                playerPosition.y = playerController.Position.y;
                playerPosition.z = playerController.Position.y;

                playerView.transform.position = playerPosition;
            }
        }
    }
}
