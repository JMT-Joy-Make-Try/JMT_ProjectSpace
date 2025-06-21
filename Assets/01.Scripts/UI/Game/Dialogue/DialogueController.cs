using JMT.DialogueSystem;
using JMT.QuestSystem;
using UnityEngine;

namespace JMT.UISystem.Dialogue
{
    public struct DialogueData
    {
        public QuestSO Quest;
        public string Range;
        IDialogueHandler handler;
    }
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private DialogueView view;
        private DialogueModel model = new();
    }
}
