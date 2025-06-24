using JMT.UISystem;
using JMT.UISystem.Dialogue;
using UnityEngine;

namespace JMT.DialogueSystem
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [SerializeField] private DialogueController dialogueCompo;

        protected override void Awake()
        {
            base.Awake();
            dialogueCompo.OnEndEvent += HandleEndEvent;
            StartDialogue("B3:C5");
        }

        public async void StartDialogue(string range)
        {
            if (range == "" || range == null) return;
            await dialogueCompo.StartDialogue(range);
            GameUIManager.Instance.PlayerControlActive(false);
            GameUIManager.Instance.GameUICompo.ClosePanel();
        }

        private void HandleEndEvent()
        {
            GameUIManager.Instance.PlayerControlActive(true);
            GameUIManager.Instance.GameUICompo.OpenPanel();
        }
    }
}
