using JMT.UISystem;
using System.Collections;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class InteractRocketQuest : QuestBase
    {
        private Coroutine delayRoutine;

        private void Awake()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
        }

        private void OnDestroy()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
        }

        private void HandleHoldEvent(bool isHold)
        {
            if(!isHold)
            {
                if (delayRoutine != null)
                    StopCoroutine(delayRoutine);

                delayRoutine = StartCoroutine(DelayRoutine());
            }
        }

        private IEnumerator DelayRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            int chidlCount = tile.TileInteraction.transform.childCount;
            if (chidlCount <= 0)
                RunQuest();
        }
    }
}
