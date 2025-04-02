using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class WorkStatusUI : MonoBehaviour
    {
        private TextMeshProUGUI statusText;
        private Image changeItem, centerItem, resultItem;

        public void SetWorkType(WorkType type)
        {
            switch (type)
            {
                case WorkType.Create:
                    changeItem.enabled = false;
                    resultItem.enabled = false;
                    Debug.Log("센터 이미지에 아이템 이미지를 넣어줘야 합니다.");
                    break;
                case WorkType.Change:
                    Debug.Log("나중에 이미지 넣어주겠지 뭐");
                    break;
            }
        }

        public void SetStatusText(string str)
        {
            // 대충 작업끝났으면 작업완료, 안끝났으면 작업중... 이런거
            // 귀찮아서 일케만해둠.
            statusText.text = str;
        }
    }
}
