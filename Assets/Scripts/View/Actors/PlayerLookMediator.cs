using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using UnityEngine;

namespace Krk.Bum.View.Actors
{
    public class PlayerLookMediator : MonoBehaviour
    {
        [SerializeField]
        private ModelContext modelContext = null;

        [SerializeField]
        private SpriteRenderer bodyRenderer = null;

        [SerializeField]
        private SpriteRenderer bagRenderer = null;


        private PlayerLookController playerLookController;


        private void Awake()
        {
            playerLookController = modelContext.PlayerLookController;
        }

        private void Start()
        {
            var bodyId = playerLookController.CurrentBodyId;
            if (bodyId.Length > 0)
            {
                var bodyData = playerLookController.GetBody(bodyId);
                HandleBodyChanged(bodyData);
            }

            var bagId = playerLookController.CurrentBagId;
            if (bagId.Length > 0)
            {
                var bagData = playerLookController.GetBag(bagId);
                HandleBagChanged(bagData);
            }
        }

        private void OnEnable()
        {
            playerLookController.OnBodyChanged += HandleBodyChanged;
            playerLookController.OnBagChanged += HandleBagChanged;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                playerLookController.OnBodyChanged -= HandleBodyChanged;
                playerLookController.OnBagChanged -= HandleBagChanged;
            }
        }

        private void HandleBodyChanged(PlayerItemData bodyData)
        {
            bodyRenderer.sprite = bodyData.Image.Image;
            bodyRenderer.color = bodyData.Image.Color;
            bodyRenderer.transform.rotation = Quaternion.Euler(0f, 0f, bodyData.Image.Rotation);
        }

        private void HandleBagChanged(PlayerItemData bagData)
        {
            bagRenderer.sprite = bagData.Image.Image;
            bagRenderer.color = bagData.Image.Color;
            bagRenderer.transform.rotation = Quaternion.Euler(0f, 0f, bagData.Image.Rotation);
        }
    }
}
