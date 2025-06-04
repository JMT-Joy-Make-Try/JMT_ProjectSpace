using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using JMT.Planets.Field;
using System;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class FieldInteraction : TileInteraction, IItemReceivable
    {
        private SeedSO[] _seed = new SeedSO[4];
        private Plant[] _plantObject = new Plant[4];
        private int _growthStage = 1;

        public void SetSeed(SeedSO seed)
        {
            int idx = Array.FindIndex(_seed, s => s == null);
            if (idx != -1)
            {
                _seed[idx] = seed;
                _plantObject[idx] = AddObject(seed.plantObject);
            }
        }


        public void DropItem(int index)
        {
            if (_seed[index] == null) return;
            if (!_plantObject[index].IsGrowEnd) return;

            foreach (var item in _seed[index].Items)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    var itemObj = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                    itemObj.transform.position = transform.position + Vector3.up * 5f;
                    itemObj.IsCollectable = true;
                    itemObj.SetItemType(item.Key);
                }
            }
        }

        public void ReceiveItem(ItemSO item, int amount)
        {
            if (item is SeedSO seed)
            {
                SetSeed(seed);
            }
        }
    }
}