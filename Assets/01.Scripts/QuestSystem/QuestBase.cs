using JMT.Agent;
using JMT.DialogueSystem;
using JMT.Planets.Tile;
using JMT.QuestSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct TileDialogue
{
    public PlanetTile Tile;
    public string DialogueRange;
}
public class QuestBase : MonoBehaviour, IQuestTarget
{
    [SerializeField] protected List<TileDialogue> tileDialogues;
    [SerializeField] private QuestSO questData;
    public QuestSO QuestData => questData;
    public List<PlanetTile> Tiles => tileDialogues.Select(t => t.Tile).ToList();
    public List<string> Ranges => tileDialogues.Select(t => t.DialogueRange).ToList();
    public List<QuestPing> QuestPing => Tiles.Select(t => t.QuestPing).ToList();
    public QuestState QuestState { get; private set; }

    public bool IsActive { get; private set; }
    public bool CanRunQuest => QuestState == QuestState.InProgress && IsActive;
    public int CompleteCount => Tiles.Count(t => !t.QuestPing.IsEnable);
    public bool IsComplete => Tiles.All(t => !t.QuestPing.IsEnable);

    public virtual void RunQuest(int num)
    {
        QuestPing[num].DisablePing();
        QuestCountEvent();
        DialogueManager.Instance.StartDialogue(Ranges[num]);

        if (IsComplete)
        {
            QuestManager.Instance.CompleteQuest(QuestData);
            GetReward(QuestData);
        }
    }

    private void GetReward(QuestSO questData)
    {
        foreach (var rewardType in questData.questRewardTypes)
        {
            switch (rewardType)
            {
                case QuestRewardType.NPC:
                    AgentManager.Instance.AddNpc();
                    break;
                default:
                    Debug.LogError($"Unknown reward type: {rewardType}");
                    break;
            }
        }
    }

    public virtual void Enable()
    {
        QuestState = QuestState.InProgress;
        IsActive = true;
        for (int i = 0; i < tileDialogues.Count; i++)
        {
            if (tileDialogues != null && Tiles[i].QuestPing != null)
                QuestPing[i].EnablePing();
        }

        QuestCountEvent();
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
        => QuestState = state;

    private void QuestCountEvent()
    {
        Debug.Log($"{CompleteCount}/{QuestPing.Count}");
        QuestManager.Instance.OnQuestCountEvent?.Invoke($"{CompleteCount}/{QuestPing.Count}");
    }
}