using JMT.Core.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField] private QuestListSO questListSO;

        private int currentQuestIndex = 0;
        private List<IQuestTarget> currentQuestTargets = new List<IQuestTarget>();

        private void Awake()
        {
            currentQuestTargets = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IQuestTarget>().ToList();
        }
        private void Start()
        {
            Debug.Log(currentQuestTargets.Count);
            StartQuest(questListSO.quests[currentQuestIndex]);
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
                questTarget.SetState(QuestState.Completed);
                currentQuestIndex++;
                
                if (currentQuestIndex < questListSO.quests.Count)
                {
                    StartQuest(questListSO.quests[currentQuestIndex]);
                }
                else
                {
                    Debug.Log("All quests completed!");
                }
            }
        }

        private void StartQuest(QuestSO questData)
        {
            Debug.Log($"Starting quest '{questData.questName}'");

            foreach (var target in currentQuestTargets)
            {
                if (target.QuestData == questData)
                {
                    target.Enable();
                }
            }
        }
    }


}