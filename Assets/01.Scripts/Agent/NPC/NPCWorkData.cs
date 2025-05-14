using JMT.DayTime;
using System.Collections;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCWorkData : MonoBehaviour
    {
        private CreateItemSO _currentItem;
        private TimeData _timeData;
        
        public CreateItemSO CurrentItem => _currentItem;
        public TimeData TimeData => _timeData;
        
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
    }
}
