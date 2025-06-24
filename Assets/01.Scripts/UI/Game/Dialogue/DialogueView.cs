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
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private Image characterImage;

        private StringBuilder builder = new();
        private Coroutine dialogueRoutine;

        private string desc;

        private bool isEnd;
        public bool IsEnd => isEnd;

        public void SetDialogue(DialogueData data)
        {
            nameText.text = data.Name;
            descText.text = "";
            dialogueRoutine = StartCoroutine(DialogueRoutine(data.Description));
        }

        private IEnumerator DialogueRoutine(string desc)
        {
            var waitTime = new WaitForSeconds(0.04f);
            isEnd = false;
            builder.Clear();
            this.desc = desc;
            foreach (char text in desc)
            {
                builder.Append(text);
                descText.text = builder.ToString();

                yield return waitTime;
            }
            SkipDescription();
        }

        public void SkipDescription()
        {
            isEnd = true;
            if(dialogueRoutine != null)
            {
                StopCoroutine(dialogueRoutine);
                dialogueRoutine = null;
            }
            descText.text = desc;
        }
    }
}
