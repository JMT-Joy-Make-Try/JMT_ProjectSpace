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
            if (tiles == null || tiles.Count <= 0)
            {
                Debug.LogError("InteractRocketQuest: tiles is null or empty.");
                yield break;
            }
            Debug.Log(tiles[0].TileInteraction);
            int chidlCount = tiles[0].TileInteraction.transform.childCount;
            if (chidlCount <= 0)
                RunQuest(0);
        }
    }
}
