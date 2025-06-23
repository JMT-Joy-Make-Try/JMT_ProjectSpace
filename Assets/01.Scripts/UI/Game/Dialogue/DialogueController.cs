using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Dialogue
{
    public struct DialogueData
    {
        public string Name;
        public string Description;

        public DialogueData(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private DialogueView view;
        [SerializeField] private TouchScreen touchScreen;

        private DialogueModel model = new();

        private Queue<DialogueData> dialogueDatas = new();
        private bool isComplete = false;

        private void Awake()
        {
            touchScreen.OnClickEvent += HandleClickEvent;
        }

        private async void Start()
        {
            string data = await model.LoadDataAsync("A3:B4");
            dialogueDatas = model.SettingDialogueData(data);
            SetDialogue();
        }

        private void HandleClickEvent()
        {
            if (!isComplete)
            {
                isComplete = true;
                view.SkipDescription();
            }
            else
            {
                isComplete = false;
                SetDialogue();
            }
        }

        private void SetDialogue()
        {
            if (dialogueDatas.Count > 0)
            {
                view.OpenUI();
                view.SetDialogue(dialogueDatas.Dequeue());
            }
            else
            {
                view.CloseUI();
                Debug.Log("끝낫어요");
            }
        }
    }
}
