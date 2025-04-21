using JMT.Building.Component;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Planets.Tile.Items;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class OxygenBuilding : ItemBuilding
    {
        private BuildingData _data;
        private void Start()
        {
            BuildingManager.Instance.OxygenBuilding = this;
            _data = GetBuildingComponent<BuildingData>();
            StartCoroutine(WorkCoroutine());
        }

        private IEnumerator WorkCoroutine()
        {
            var ws = new WaitForSeconds(0.1f);
            while (true)
            {
                if (data.GetFirstCreateItem() == null || data.CreateItemList.Count <= 0 || data.Works.Count <= 0)
                {
                    Debug.Log("Building Data is null");
                    yield return ws;
                    continue;
                }

                if (_data.CurrentItems.Find(x => x.Item1 == ItemType.PurificationContainer).Item2 >= 3)
                {
                    yield return ws;
                    continue;
                }
                int timeMinute = data.GetFirstCreateItem().CreateTime.minute * 60 + data.GetFirstCreateItem().CreateTime.second;
                yield return new WaitForSeconds(timeMinute);
                data.RemoveWork();
            }
        }

        public bool GetOxygen()
        {
            var item = _data.CurrentItems.Find(s => s.Item1 == ItemType.PurificationContainer);
            if (item == null)
            {
                return false;
            }
            else if (item.Item2 <= 0)
            {
                _data.CurrentItems.Remove(item);
            }
            else
            {
                item.Item2--;
            }
            return true;
        }
    }
}