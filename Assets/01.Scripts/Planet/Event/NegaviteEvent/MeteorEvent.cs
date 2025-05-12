using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT
{
    public class MeteorEvent : Event
    {
        [SerializeField] private GameObject meteorPrefab;
        public override void StartEvent()
        {
            Debug.Log("운석이 떨어집니다!!!");
            GameObject go = Instantiate(meteorPrefab, TileAreaManager.Instance.GetTile(30).position, Quaternion.identity);
            Debug.Log(go.name);
        }
    }
}
