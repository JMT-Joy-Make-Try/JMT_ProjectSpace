using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Object;
using JMT.Planets.Field;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class FieldInteraction : TileInteraction
    {
        private SeedSO _seed;
        private int _growthStage = 1;
        
        private GameObject _plantObject;
        
        public override void Interaction()
        {
            base.Interaction();
            if (_seed == null) return;
            
            if (_growthStage > _seed.MaxGrowthStage)
            {
                Debug.Log("Plant is fully grown.");
                return;
            }
        }
        
        public void SetSeed(SeedSO seed)
        {
            _seed = seed;
            AddObject(seed.SeedObjects[0]);
        }

        private void GrowSeed()
        {
            if (_seed == null) return;
            _growthStage = Mathf.Clamp(_growthStage++, 1, _seed.MaxGrowthStage);
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
                AddObject(_seed.SeedObjects[_growthStage - 1]);
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
    }
}