using Krk.Bum.Model;
using Krk.Bum.Model.Context;
using Krk.Bum.Model.Core;
using Krk.Bum.View.Street;
using UnityEngine;

namespace Krk.Bum.View.Sounds
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private SoundPlayerConfig config = null;

        [SerializeField]
        private TrashMediator trashMediator = null;

        [SerializeField]
        private ModelContext modelContext = null;


        private ModelController modelController;


        private void Awake()
        {
            modelController = modelContext.ModelController;
        }
        
        private void OnEnable()
        {
            trashMediator.OnTrashHit += HandleTrashHit;

            modelController.OnCollectionUnlocked += HandleCollectionUnlocked;
            modelController.OnItemCreated += HandleItemCreated;
            modelController.OnItemSold += HandleItemSold;
            modelController.OnPartCollected+= HandlePartCollected;
        }

        private void HandlePartCollected(PartData arg0)
        {
            PlayRandom(config.CollectPart);
        }

        private void HandleItemSold(ItemData arg0)
        {
            PlayRandom(config.SellItem);
        }

        private void HandleItemCreated(ItemData arg0)
        {
            PlayRandom(config.CraftItem);
        }

        private void HandleCollectionUnlocked(CollectionData arg0)
        {
            PlayRandom(config.UnlockCollection);
        }

        private void HandleTrashHit()
        {
            PlayRandom(config.TrashHits);
        }

        private void PlayRandom(AudioClip[] clips)
        {
            var hit = clips[Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(hit);
        }
    }
}
