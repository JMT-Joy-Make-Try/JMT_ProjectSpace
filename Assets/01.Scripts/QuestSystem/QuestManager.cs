using JMT.Core.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        public event Action<QuestSO> OnQuestStartEvent;
        public event Action OnQuestEndEvent;

        [SerializeField] private List<QuestSO> pingDatas = new();

        private int currentQuestIndex = 0;
        private List<IQuestTarget> currentQuestTargets = new List<IQuestTarget>();

        protected override void Awake()
        {
            base.Awake();
            currentQuestTargets = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IQuestTarget>().ToList();
        }
        private void Start()
        {
            Debug.Log(currentQuestTargets.Count);
            StartQuest(pingDatas[currentQuestIndex]);
        }

        public void CompleteQuest(QuestSO questData)
        {
            if (questData == null)
            {
                Debug.LogError("Quest data is null!");
                return;
            }
            
            var questTarget = currentQuestTargets.FirstOrDefault(target => target.QuestData == questData);
            
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
            Debug.Log($"Starting quest '{questData.questName}'");

            foreach (var target in currentQuestTargets)
            {
                if (target.QuestData == questData)
                {
                    OnQuestStartEvent?.Invoke(questData);
                    target.Enable();
                }
            }
        }

        private IEnumerator DelayQuestRoutine()
        {
            currentQuestIndex++;

            if (currentQuestIndex < pingDatas.Count)
            {
                yield return new WaitForSeconds(1f);
                StartQuest(pingDatas[currentQuestIndex]);
            }
            else
                Debug.Log("All quests completed!");

            yield return null;
        }
    }
}