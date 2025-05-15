using JMT.Agent.NPC;
using JMT.DayTime;
using System.Collections;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCWorkData : MonoBehaviour, INPCComponent
    {
        private CreateItemSO _currentItem;
        private TimeData _timeData;
        
        public CreateItemSO CurrentItem => _currentItem;
        public TimeData TimeData => _timeData;
        public NPCAgent Agent { get; private set; }
        
        public void SetData(CreateItemSO item, TimeData time)
        {
            _currentItem = item;
            _timeData = time;
            
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

        public int GetStatus()
        {
            // 건강 바탕으로 return
            // 0: 건강 좋음
            // 1: 건강 중간
            // 2: 건강 나쁨

            return 0;
        }

        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }
    }
}
