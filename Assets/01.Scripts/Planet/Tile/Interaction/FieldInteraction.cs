using JMT.Agent;
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
    public class FieldInteraction : TileInteraction
    {
        private Field.Field _field;
        private SeedSO[] _seed = new SeedSO[4];
        private Plant[] _plantObject = new Plant[4];
        private int _growthStage = 1;

        public void SetField(Field.Field field)
        {
            _field = field;
        }

        public override void Interaction()
        {
            base.Interaction();
            var inventory = AgentManager.Instance.Player.InventoryCompo;
            var currentItem = inventory.PlayerInventoryData.item;
            if (currentItem is not SeedSO) return;
            
            if (Array.Exists(_seed, s => s == null))
            {
                SetSeed(currentItem as SeedSO);
            }
            else
            {
                Debug.Log("All fields are occupied.");
                return;
            }
        }

        public void SetSeed(SeedSO seed)
        {
            int idx = Array.FindIndex(_seed, s => s == null);
            if (idx != -1)
            {
                _seed[idx] = seed;
                _plantObject[idx] = AddObject(seed.plantObject, _field.PlantPositions[idx]);
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

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (item is SeedSO seed)
            {
                SetSeed(seed);
            }

            return true;
        }
    }
}