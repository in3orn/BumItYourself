using System.Collections.Generic;
using UnityEngine.Events;

namespace Krk.Bum.View.Street
{
    public class BlocksController
    {
        public UnityAction<BlockView, BlockData> OnBlockSpawned;


        private readonly BlocksControllerConfig config;

        private readonly List<BlockData> leftBlocks;
        private readonly List<BlockData> rightBlocks;

        private float currentX;


        public BlocksController(BlocksControllerConfig config)
        {
            this.config = config;

            leftBlocks = new List<BlockData>();
            rightBlocks = new List<BlockData>();
        }


        public void UpdatePosition(float x)
        {
            currentX = x;
        }

        public bool ShouldSpawnLeftBlock()
        {
            if (leftBlocks.Count <= 0)
            {
                return currentX < config.SpawnDistance;
            }

            var lastBlock = leftBlocks[leftBlocks.Count - 1];
            return currentX - lastBlock.CenterX - lastBlock.HalfWidth < config.SpawnDistance;
        }

        public bool ShouldSpawnRightBlock()
        {
            if (rightBlocks.Count <= 0)
            {
                return -currentX < config.SpawnDistance;
            }

            var lastBlock = rightBlocks[rightBlocks.Count - 1];
            return lastBlock.CenterX + lastBlock.HalfWidth - currentX < config.SpawnDistance;
        }

        public BlockData SpawnLeftBlock(BlockView blockView)
        {
            var halfWidth = blockView.Width / 2f;
            var data = new BlockData
            {
                HalfWidth = halfWidth,
                CenterX = GetLeftEnd() - halfWidth
            };
            leftBlocks.Add(data);
            if(OnBlockSpawned != null) OnBlockSpawned(blockView, data);
            return data;
        }

        private float GetLeftEnd()
        {
            if (leftBlocks.Count <= 0) return config.SpawnIntervalRange.GetRandom();
            var lastBlock = leftBlocks[leftBlocks.Count - 1];
            return lastBlock.CenterX - lastBlock.HalfWidth - config.SpawnIntervalRange.GetRandom();
        }

        public BlockData SpawnRightBlock(BlockView blockView)
        {
            var halfWidth = blockView.Width / 2f;
            var data = new BlockData
            {
                HalfWidth = halfWidth,
                CenterX = GetRightEnd() + halfWidth
            };
            rightBlocks.Add(data);
            if (OnBlockSpawned != null) OnBlockSpawned(blockView, data);
            return data;
        }

        private float GetRightEnd()
        {
            if (rightBlocks.Count <= 0) return config.SpawnIntervalRange.GetRandom();
            var lastBlock = rightBlocks[rightBlocks.Count - 1];
            return lastBlock.CenterX + lastBlock.HalfWidth + config.SpawnIntervalRange.GetRandom();
        }
    }
}
