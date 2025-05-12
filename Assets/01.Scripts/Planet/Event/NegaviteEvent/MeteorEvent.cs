using JMT.Object;
using JMT.Planets.Tile;
using JMT.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT
{
    public class MeteorEvent : Event
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        [SerializeField] private Meteor meteorPrefab;
        [SerializeField] private int meteorCount = 5;
        [SerializeField] private float range = 10f;
        
        private List<PlanetTile> tileList = new List<PlanetTile>();
        
        public override void StartEvent()
        {
            StartCoroutine(MeteorRain());
            
        }

        private IEnumerator MeteorRain()
        {
            tileList.Clear();
            GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("운석이 떨어집니다!!!");
            for (int i = 0; i < meteorCount; i++)
            {
                // 타일을 랜덤으로 선택
                var tile = TileAreaManager.Instance.GetTile(range);
                if (tile != null)
                {
                    tileList.Add(tile.GetComponent<PlanetTile>());
                    tile.GetComponent<PlanetTile>().Renderer.material.SetColor(BaseColor, Color.red);
                }

                yield return null;
            }
            
            // 타일이 빨개져야함.
            yield return new WaitForSeconds(2f);
            foreach (var tile in tileList)
            {
                Instantiate(meteorPrefab, tile.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
            
            // 타일이 원래 색으로 돌아와야함.
            foreach (var tile in tileList)
            {
                tile.Renderer.material.SetColor(BaseColor, Color.white);
            }
        }
    }
}
