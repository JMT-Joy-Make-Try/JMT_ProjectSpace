using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Dialogue
{
    public class DialogueView : PanelUI
    {
        public event Action<bool> OnCompleteEvent;

        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;

        private StringBuilder builder = new();
        private Coroutine dialogueRoutine;

        private string desc;

        public void SetDialogue(DialogueData data)
        {
            nameText.text = data.name;
            dialogueRoutine = StartCoroutine(DialogueRoutine(data.desc));
        }

        private IEnumerator DialogueRoutine(string desc)
        {
            OnCompleteEvent?.Invoke(false);
            var waitTime = new WaitForSeconds(0.06f);
            builder?.Clear();
            this.desc = desc;
            foreach (char text in desc)
            {
                builder.Append(text);
                descText.text = builder.ToString();

                yield return waitTime;
            }
            ShowAllDescription();
        }

        public void ShowAllDescription()
        {
            if(dialogueRoutine != null)
            {
                StopCoroutine(dialogueRoutine);
                dialogueRoutine = null;
            }
            descText.text = desc;
            OnCompleteEvent?.Invoke(true);
        }
    }
}
