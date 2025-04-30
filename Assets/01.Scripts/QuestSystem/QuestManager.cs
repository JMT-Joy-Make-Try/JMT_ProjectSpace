using System;
using System.Collections;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private QuestListSO _questListSO;

        private QuestSO _currentQuestSO;
        private void Start()
        {
            _questListSO = _questListSO.Clone() as QuestListSO;
        }

        // _questListSO를 사용해서 퀘스트를 진행
        // _questListSO의 List 순서대로 진행된다.
        // 퀘스트를 진행할 때마다 _currentQuestSO에 현재 퀘스트를 저장한다.
        // 퀘스트를 완료하면 다음 퀘스트로 넘어간다.
    }
}