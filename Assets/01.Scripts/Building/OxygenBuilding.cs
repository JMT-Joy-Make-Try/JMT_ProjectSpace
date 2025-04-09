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
        public override void Build(Vector3 position, Transform parent)
        {
        }

        private void Start()
        {
            BuildingManager.Instance.OxygenBuilding = this;
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
                int timeMinute = data.GetFirstCreateItem().CreateTime.minute * 60 + data.GetFirstCreateItem().CreateTime.second;
                yield return new WaitForSeconds(timeMinute);
                data.RemoveWork();
            }
        }

        public bool GetOxygen()
        {
            var item = CurrentItems.Find(s => s.Item1 == ItemType.PurificationContainer);
            if (item == null)
            {
                return false;
            }
            else if (item.Item2 <= 0)
            {
                CurrentItems.Remove(item);
            }
            else
            {
                item.Item2--;
            }
            return true;
        }
    }
}