using JMT.QuestSystem;
using System;
using UnityEngine;

public class TestObj : MonoBehaviour, IQuestTarget
{
    [field: SerializeField] public QuestSO QuestData { get; private set; }
    public QuestState QuestState { get; private set; }
    public bool IsActive { get; private set; }

    public void RunQuest()
    {
        QuestManager.Instance.CompleteQuest(QuestData);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        QuestState = QuestState.InProgress;
        IsActive = true;
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
    {
        QuestState = state;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && QuestState == QuestState.InProgress && IsActive)
        {
            RunQuest();
        }
    }
}