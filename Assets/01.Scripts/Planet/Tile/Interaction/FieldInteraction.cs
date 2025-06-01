using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Object;
using JMT.Planets.Field;
using System;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class FieldInteraction : TileInteraction
    {
        [SerializeField] private SeedSO debugSeed;
        private SeedSO _seed;
        private int _growthStage = 1;
        
        private GameObject _plantObject;
        
        public override void Interaction()
        {
            base.Interaction();
        }
        
        public void SetSeed(SeedSO seed)
        {
            _seed = seed;
            _plantObject = AddObject(seed.SeedObjects[0]);
        }

        private void GrowSeed()
        {
            if (_seed == null) return;
            _growthStage++;
            if (_growthStage > _seed.MaxGrowthStage)
            {
                Debug.Log("Plant is fully grown.");
                DropItem();
                Destroy(_plantObject);
                return;
            }
            ChangePlantObject();
            
            
        }

        private void ChangePlantObject()
        {
            if (_plantObject != null)
            {
                Destroy(_plantObject);
            }

            if (_seed != null && _seed.SeedObjects.Length >= _growthStage)
            {
                _plantObject = AddObject(_seed.SeedObjects[_growthStage - 1]);
            }
        }

        public void DropItem()
        {
            if (_seed == null) return;

            foreach (var item in _seed.Items)
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GrowSeed();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SetSeed(debugSeed);
            }
        }
    }
}