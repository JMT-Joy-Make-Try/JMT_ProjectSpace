using AYellowpaper.SerializedCollections;
using JMT.QuestSystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<QuestSO, string> questDialogue;
        [SerializeField] private QuestSO testQuest;
        [SerializeField] private DialogueView view;
        [SerializeField] private TouchScreen touchView;

        private Queue<DialogueData> dialogueDatas = new();

        private DialogueModel model = new();

        private bool isComplete;
        private void Awake()
        {
            touchView.OnClickEvent += HandleClickEvent;
            view.OnCompleteEvent += HandleCompleteEvent;
        }

        private void Start()
        {
            StartQuest(testQuest);
        }

        private void HandleClickEvent()
        {
            Debug.Log("눌렸어요");
            if (!isComplete)
                view.ShowAllDescription();
            else
                StartDialogue();
        }

        private void HandleCompleteEvent(bool isComplete)
        {
            this.isComplete = isComplete;
        }


        public async void StartQuest(QuestSO quest)
        {
            questDialogue.TryGetValue(quest, out string range);
            string data = await model.LoadDataAsync(range);
            dialogueDatas = model.SettingDialogueData(data);
            StartDialogue();
        }

        public async void StartQuest(string range)
        {
            string data = await model.LoadDataAsync(range);
            dialogueDatas = model.SettingDialogueData(data);
            StartDialogue();
        }

        private void StartDialogue()
        {
            if (dialogueDatas.Count > 0)
            {
                view.SetDialogue(dialogueDatas.Dequeue());
                view.OpenUI();
            }
            else
            {
                Debug.Log("끝났어요");
                view.CloseUI();
            }
        }
    }
}
