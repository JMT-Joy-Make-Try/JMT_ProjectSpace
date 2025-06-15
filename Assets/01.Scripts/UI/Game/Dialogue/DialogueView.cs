using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Dialogue
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private Image characterImage;

        private StringBuilder builder;
        private Coroutine dialogueRoutine;

        public void SetDialogue(string name, string desc)
        {
            nameText.text = name;
            dialogueRoutine = StartCoroutine(DialogueRoutine(desc));
        }

        private IEnumerator DialogueRoutine(string desc)
        {
            var waitTime = new WaitForSeconds(0.05f);
            builder.Clear();
            foreach (char text in desc)
            {
                builder.Append(text);
                descText.text = builder.ToString();

                yield return waitTime;
            }
            SetDescription(desc);
        }

        public void SetDescription(string desc)
        {
            if(dialogueRoutine != null)
            {
                StopCoroutine(dialogueRoutine);
                dialogueRoutine = null;
            }
            descText.text = desc;
        }
    }
}
