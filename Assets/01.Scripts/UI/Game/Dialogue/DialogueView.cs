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

            Stack<string> tagStack = new();
            StringBuilder visibleText = new();

            for (int i = 0; i < desc.Length; i++)
            {
                char c = desc[i];
                if (c == '<')
                {
                    int tagEnd = desc.IndexOf('>', i);
                    if (tagEnd == -1) break;

                    string fullTag = desc.Substring(i, tagEnd - i + 1);
                    bool isClosing = fullTag.StartsWith("</");

                    if (!isClosing)
                    {
                        tagStack.Push(fullTag);
                    }
                    else
                    {
                        if (tagStack.Count > 0) tagStack.Pop();
                    }

                    visibleText.Append(fullTag);
                    i = tagEnd;
                    continue;
                }

                visibleText.Append(c);

                builder.Clear();
                builder.Append(visibleText.ToString());

                foreach (var tag in tagStack)
                {
                    string closing = "</" + tag.Substring(1); // <b> -> </b>
                    builder.Append(closing);
                }

                descText.text = builder.ToString();
                yield return waitTime;
            }

            dialogueRoutine = null;
            isEnd = true;
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
