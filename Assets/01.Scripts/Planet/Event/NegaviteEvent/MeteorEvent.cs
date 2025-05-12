using JMT.Object;
using JMT.UISystem;
using System.Collections;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT
{
    public class MeteorEvent : Event
    {
        [SerializeField] private Meteor meteorPrefab;
        [SerializeField] private int meteorCount = 5;
        
        public override void StartEvent()
        {
            StartCoroutine(MeteorRain());
            
        }

        private IEnumerator MeteorRain()
        {
            GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("운석이 떨어집니다!!!");
            for (int i = 0; i < meteorCount; i++)
            {
                Meteor go = Instantiate(meteorPrefab, TileAreaManager.Instance.GetTile(30).position, Quaternion.identity);
                Debug.Log(go.name);
                yield return new WaitForSeconds(Random.Range(3f, 5f));
            }
        }
    }
}
