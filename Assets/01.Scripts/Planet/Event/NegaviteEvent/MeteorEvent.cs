using JMT;
using JMT.Android.Vibration;
using JMT.CameraSystem;
using JMT.Object;
using JMT.Planets.Tile;
using JMT.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace Planets.Events
{
    public class MeteorEvent : Event
    {
        [SerializeField] private Meteor meteorPrefab;
        [SerializeField] private int meteorCount = 5;
        [SerializeField] private float range = 10f;
        
        private List<PlanetTile> tileList = new List<PlanetTile>();
        
        public override void StartEvent()
        {
            Debug.Log("MeteorEvent");
            StartCoroutine(MeteorRain());
            //CameraManager.Instance.ShakeCamera(2, 5);
        }

        public override void EndEvent()
        {
            StopAllCoroutines();
            foreach (var tile in tileList)
            {
                tile.SetColor(Color.white);
            }
            tileList.Clear();
        }

        private IEnumerator MeteorRain()
        {
            tileList.Clear();
            GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("운석이 떨어집니다!!!");
            for (int i = 0; i < meteorCount; i++)
            {
                var tile = TileAreaManager.Instance.GetTile(range);
                if (tile != null)
                {
                    PlanetTile planetTile = tile.GetComponent<PlanetTile>();
                    tileList.Add(planetTile);
                    planetTile.SetColor(Color.red);
                }

                yield return null;
            }
            
            yield return new WaitForSeconds(2f);
            foreach (var tile in tileList)
            {
                Instantiate(meteorPrefab, tile.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(4.5f);
            EndEvent();
        }
    }
}