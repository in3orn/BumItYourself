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

        [SerializeField]
        private SpriteRenderer stickRenderer = null;

        [SerializeField]
        private SpriteRenderer glassesRenderer = null;

        [SerializeField]
        private SpriteRenderer beardRenderer = null;


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
                var data = playerLookController.GetBody(bodyId);
                HandleBodyChanged(data);
            }

            var bagId = playerLookController.CurrentBagId;
            if (bagId.Length > 0)
            {
                var data = playerLookController.GetBag(bagId);
                HandleBagChanged(data);
            }

            var stickId = playerLookController.CurrentStickId;
            if (stickId.Length > 0)
            {
                var data = playerLookController.GetStick(stickId);
                HandleStickChanged(data);
            }

            var glassesId = playerLookController.CurrentGlassesId;
            if (glassesId.Length > 0)
            {
                var data = playerLookController.GetGlasses(glassesId);
                HandleGlassesChanged(data);
            }
        }

        private void OnEnable()
        {
            playerLookController.OnBodyChanged += HandleBodyChanged;
            playerLookController.OnBagChanged += HandleBagChanged;
            playerLookController.OnStickChanged += HandleStickChanged;
            playerLookController.OnGlassesChanged += HandleGlassesChanged;
            playerLookController.OnBeardChanged += HandleBeardChanged;
        }

        private void OnDisable()
        {
            if (modelContext != null)
            {
                playerLookController.OnBodyChanged -= HandleBodyChanged;
                playerLookController.OnBagChanged -= HandleBagChanged;
                playerLookController.OnStickChanged -= HandleStickChanged;
                playerLookController.OnGlassesChanged -= HandleGlassesChanged;
                playerLookController.OnBeardChanged -= HandleBeardChanged;
            }
        }

        private void HandleBodyChanged(PlayerItemData data)
        {
            SetImage(bodyRenderer, data.Image);
        }

        private void HandleBagChanged(PlayerItemData data)
        {
            SetImage(bagRenderer, data.Image);
        }

        private void HandleStickChanged(PlayerItemData data)
        {
            SetImage(stickRenderer, data.Image);
        }

        private void HandleGlassesChanged(PlayerItemData data)
        {
            SetImage(glassesRenderer, data.Image);
        }

        private void HandleBeardChanged(PlayerItemData data)
        {
            SetImage(beardRenderer, data.Image);
        }

        private void SetImage(SpriteRenderer renderer, ImageData data)
        {
            renderer.sprite = data.Image;
            renderer.color = data.Color;
            renderer.transform.rotation = Quaternion.Euler(0f, 0f, data.Rotation);
        }
    }
}
