using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        public event Action<QuestSO> OnQuestStartEvent;
        public event Action OnQuestEndEvent;

        [SerializeField] private List<QuestSO> questSOs = new();

        private int currentQuestIndex = 0;
        private List<QuestBase> currentQuestTargets = new();
        private bool _isDelayRunning = false;

        private bool _isAllQuestCompleted;
        private bool _isStaringQuest = false;

        protected override void Awake()
        {
            base.Awake();
            currentQuestTargets = FindObjectsByType<QuestBase>(FindObjectsSortMode.None).ToList();
        }
        private void Start()
        {
            StartQuest(questSOs[currentQuestIndex]);
        }

        public void CompleteQuest(QuestSO questData)
        {
            if (_isAllQuestCompleted) 
                return;
            if (questData == null)
            {
                Debug.LogError("Quest data is null!");
                return;
            }
            
            var questTarget = currentQuestTargets.FirstOrDefault(target => target.QuestData == questData);
            GameUIManager.Instance.PointerCompo.ClosePointerUI();

            if (questTarget != null)
            {
                Debug.Log($"Quest '{questData.questName}' completed!");
                OnQuestEndEvent?.Invoke();
                questTarget.SetState(QuestState.Completed);
                StartCoroutine(DelayQuestRoutine());
            }
        }

        

        private void StartQuest(QuestSO questData)
        {
            if (_isAllQuestCompleted) 
                return;
            if (_isStaringQuest)
            {
                Debug.LogWarning("Quest is already starting!");
                return;
            }
            Debug.Log($"Starting quest '{questData.questName}'");

            foreach (var target in currentQuestTargets)
            {
                if (target.QuestData == questData)
                {
                    OnQuestStartEvent?.Invoke(questData);
                    if (target.Tiles != null)
                    {
                        Debug.Log("핑...");
                        //GameUIManager.Instance.PointerCompo.SetPointer(target.Tiles.Pivot);
                    }
                    _isStaringQuest = true;
                    target.Enable();
                }
            }
        }

        private IEnumerator DelayQuestRoutine()
        {
            if (_isAllQuestCompleted || _isDelayRunning) 
                yield break;

            _isDelayRunning = true;
            _isStaringQuest = false;
            
            if (currentQuestIndex >= questSOs.Count)
            {
                _isAllQuestCompleted = true;
                Debug.Log("All quests completed!");
            }

            currentQuestIndex++;

            if ( currentQuestIndex < questSOs.Count )
            {
                yield return new WaitForSeconds(1f);
                StartQuest(questSOs[currentQuestIndex]);
            }
            

            _isDelayRunning = false;
            yield return null;
        }
    }
}