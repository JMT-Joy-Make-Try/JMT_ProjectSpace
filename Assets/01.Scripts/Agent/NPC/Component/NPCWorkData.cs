using JMT.Agent.NPC;
using JMT.Core.Tool;
using JMT.DayTime;
using System.Collections;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCWorkData : MonoBehaviour, INPCComponent
    {
        private CreateItemSO _currentItem;
        private TimeData _timeData;
        private int _itemCount;
        
        public CreateItemSO CurrentItem => _currentItem;
        public TimeData TimeData => _timeData;
        public string ItemCount => _itemCount.ToString();
        public NPCAgent Agent { get; private set; }
        
        public void SetData(CreateItemSO item, TimeData time, int itemCount)
        {
            _currentItem = item;
            _timeData = time;
            _itemCount = itemCount;
            
            StartCoroutine(SetTimeData());
        }

        private IEnumerator SetTimeData()
        {
            while (_timeData.GetSecond() != 0)
            {
                _timeData.second--;
                if (_timeData.second < 0)
                {
                    _timeData.second = 59;
                    _timeData.minute--;
                }
                yield return new WaitForSeconds(1);
            }
        }

        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }
    }
}
