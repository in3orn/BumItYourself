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


        private PlayerLookController playerLookController;


        private void Awake()
        {
            playerLookController = modelContext.PlayerLookController;
        }

        private void Start()
        {
            var bodyId = playerLookController.CurrentBodyId;
            var bodyData = playerLookController.GetItem(bodyId);
            HandleBodyChanged(bodyData);
        }

        private void OnEnable()
        {
            playerLookController.OnBodyChanged += HandleBodyChanged;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                playerLookController.OnBodyChanged -= HandleBodyChanged;
            }
        }

        private void HandleBodyChanged(PlayerItemData bodyData)
        {
            bodyRenderer.sprite = bodyData.Image.Image;
            bodyRenderer.color = bodyData.Image.Color;
            bodyRenderer.transform.rotation = Quaternion.Euler(0f, 0f, bodyData.Image.Rotation);
        }
    }
}
