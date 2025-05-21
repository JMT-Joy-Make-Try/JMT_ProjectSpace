using JMT.Agent;
using JMT.Core.Tool;
using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.QuestSystem
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        public event Action<QuestSO> OnQuestStartEvent;
        public event Action OnQuestEndEvent;

        [SerializeField] private List<ChapterSO> chapterSO = new();

        private int currentQuestIndex = 0;
        private int currentChapterIndex = 0;
        private List<QuestBase> currentQuestTargets = new();
        private bool _isDelayRunning = false;

        private bool _isAllQuestCompleted;

        protected override void Awake()
        {
            base.Awake();
            currentQuestTargets = FindObjectsByType<QuestBase>(FindObjectsSortMode.None).ToList();
        }
        private void Start()
        {
            Debug.Log(currentQuestTargets.Count);
            StartQuest(chapterSO[currentChapterIndex].quests[currentQuestIndex]);
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
            Debug.Log($"Starting quest '{questData.questName}'");

            foreach (var target in currentQuestTargets)
            {
                if (target.QuestData == questData)
                {
                    OnQuestStartEvent?.Invoke(questData);
                    if (target.Tile != null)
                    {
                        GameUIManager.Instance.PointerCompo.SetPointer(target.Tile.Pivot);
                    }

                    target.Enable();
                }
            }
        }

        private IEnumerator DelayQuestRoutine()
        {
            if (_isAllQuestCompleted || _isDelayRunning) 
                yield break;

            _isDelayRunning = true;
            
            if (currentChapterIndex >= chapterSO.Count)
            {
                _isAllQuestCompleted = true;
                Debug.Log("All quests completed!");
            }

            currentQuestIndex++;
            if (currentQuestIndex >= chapterSO[currentChapterIndex].quests.Count)
            {
                currentQuestIndex = 0;
                currentChapterIndex++;
            }

            if (currentChapterIndex < chapterSO.Count &&currentQuestIndex < chapterSO[currentChapterIndex].quests.Count )
            {
                yield return new WaitForSeconds(1f);
                StartQuest(chapterSO[currentChapterIndex].quests[currentQuestIndex]);
            }
            

            _isDelayRunning = false;
            yield return null;
        }
    }
}