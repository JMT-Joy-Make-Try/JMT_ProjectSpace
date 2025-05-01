using JMT;
using JMT.Planets.Tile;
using JMT.QuestSystem;
using System;
using UnityEngine;

public class TestObj : MonoBehaviour, IQuestTarget
{
    [SerializeField] private PlanetTile tile;
    [field: SerializeField] public QuestSO QuestData { get; private set; }
    public QuestState QuestState { get; private set; }
    public bool IsActive { get; private set; }

    public bool CanRunQuest => QuestState == QuestState.InProgress && IsActive;

    public QuestPing QuestPing => tile.QuestPing;

    public void RunQuest()
    {
        QuestPing.DisablePing();
        QuestManager.Instance.CompleteQuest(QuestData);
    }

    public void Enable()
    {
        QuestState = QuestState.InProgress;
        IsActive = true;
        QuestPing.EnablePing();
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
    {
        QuestState = state;
    }

    private void Update()
    {
        Debug.Log("asdf");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("CanRunQuest : " + CanRunQuest);
            if (CanRunQuest)
                RunQuest();
        }
    }
}