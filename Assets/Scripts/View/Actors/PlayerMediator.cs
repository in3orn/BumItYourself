using Krk.Bum.Game.Actors;
using Krk.Bum.Game.Context;
using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class PlayerMediator : MonoBehaviour
    {
        [SerializeField]
        private PlayerView playerView = null;

        [SerializeField]
        private GameContext gameContext = null;


        private PlayerController playerController;


        private void Awake()
        {
            playerController = gameContext.PlayerController;
        }

        private void Update()
        {
            playerController.Update(Time.deltaTime);

            if ((Vector2)playerView.transform.position != playerController.Position)
            {
                playerView.transform.position = playerController.Position;
            }
        }
    }
}
