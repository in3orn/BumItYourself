using System.Collections.Generic;
using Krk.Bum.Model;
using Krk.Bum.View.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Bum.View.Screens
{
    public class SummaryScreenView : ScreenView
    {
        public Button CollectButton;

        [SerializeField]
        private PartTile partTile = null;

        [SerializeField]
        private RectTransform itemsContent = null;


        private readonly List<PartTile> partTiles;

        public List<PartTile> PartTiles { get { return partTiles; } }


        public SummaryScreenView()
        {
            partTiles = new List<PartTile>();
        }

        public void Init(List<PartData> items)
        {
            var size = Mathf.Min(items.Count, partTiles.Count);

            DisableItems(size);
            UpdateItems(items, size);
            CreateItems(items, size);
        }

        private void CreateItems(List<PartData> items, int size)
        {
            for (int i = size; i < items.Count; i++)
            {
                var gameObject = Instantiate(partTile, itemsContent);
                var tile = gameObject.GetComponent<PartTile>();
                tile.Init(items[i]);
                partTiles.Add(tile);
            }
        }

        private void UpdateItems(List<PartData> items, int size)
        {
            for (int i = 0; i < size; i++)
            {
                partTiles[i].gameObject.SetActive(true);
                partTiles[i].Init(items[i]);
            }
        }

        private void DisableItems(int size)
        {
            for (int i = size; i < partTiles.Count; i++)
            {
                partTiles[i].gameObject.SetActive(false);
            }
        }
    }
}
