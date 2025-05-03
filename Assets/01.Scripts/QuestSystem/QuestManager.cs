using JMT.Core.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.QuestSystem
{
    [Serializable]
    public struct PingData
    {
        public QuestSO so;
        public GameObject pingTile;
    }
    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField] private List<PingData> pingDatas = new List<PingData>();

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
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
                StartQuest(pingDatas[currentQuestIndex].so);
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


                
                if (currentQuestIndex < pingDatas.Count)
                {
                    StartQuest(pingDatas[currentQuestIndex].so);
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