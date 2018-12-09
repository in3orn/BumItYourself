using System.Collections.Generic;
using UnityEngine;
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
        
        public BlockData SpawnFirstBlock(BlockView blockView)
        {
            var halfWidth = blockView.Width / 2f;

            var y = config.SpawnYRange.GetRandom();
            var z = config.InheritSpawnZFromY ? y : config.SpawnZ;

            var data = new BlockData
            {
                HalfWidth = halfWidth,
                Center = new Vector3(0f, y, z)
            };
            leftBlocks.Add(data);
            rightBlocks.Add(data);
            if (OnBlockSpawned != null) OnBlockSpawned(blockView, data);
            return data;
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
            return currentX - lastBlock.Center.x - lastBlock.HalfWidth < config.SpawnDistance;
        }

        public bool ShouldSpawnRightBlock()
        {
            if (rightBlocks.Count <= 0)
            {
                return -currentX < config.SpawnDistance;
            }

            var lastBlock = rightBlocks[rightBlocks.Count - 1];
            return lastBlock.Center.x + lastBlock.HalfWidth - currentX < config.SpawnDistance;
        }

        public BlockData SpawnLeftBlock(BlockView blockView)
        {
            var halfWidth = blockView.Width / 2f;

            var y = config.SpawnYRange.GetRandom();
            var z = config.InheritSpawnZFromY ? y : config.SpawnZ;

            var data = new BlockData
            {
                HalfWidth = halfWidth,
                Center = new Vector3(GetLeftEnd() - halfWidth, y, z)
            };
            leftBlocks.Add(data);
            if(OnBlockSpawned != null) OnBlockSpawned(blockView, data);
            return data;
        }

        private float GetLeftEnd()
        {
            if (leftBlocks.Count <= 0) return config.SpawnIntervalRange.GetRandom();
            var lastBlock = leftBlocks[leftBlocks.Count - 1];
            return lastBlock.Center.x - lastBlock.HalfWidth - config.SpawnIntervalRange.GetRandom();
        }

        public BlockData SpawnRightBlock(BlockView blockView)
        {
            var halfWidth = blockView.Width / 2f;

            var y = config.SpawnYRange.GetRandom();
            var z = config.InheritSpawnZFromY ? y : config.SpawnZ;

            var data = new BlockData
            {
                HalfWidth = halfWidth,
                Center = new Vector3(GetRightEnd() + halfWidth, y, z)
            };
            rightBlocks.Add(data);
            if (OnBlockSpawned != null) OnBlockSpawned(blockView, data);
            return data;
        }

        private float GetRightEnd()
        {
            if (rightBlocks.Count <= 0) return config.SpawnIntervalRange.GetRandom();
            var lastBlock = rightBlocks[rightBlocks.Count - 1];
            return lastBlock.Center.x + lastBlock.HalfWidth + config.SpawnIntervalRange.GetRandom();
        }
    }
}
