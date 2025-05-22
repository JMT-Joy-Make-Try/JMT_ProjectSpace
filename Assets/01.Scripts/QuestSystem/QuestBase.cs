using JMT.Agent;
using JMT.Planets.Tile;
using JMT.QuestSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBase : MonoBehaviour, IQuestTarget
{
    [SerializeField] protected List<PlanetTile> tiles;
    [SerializeField] private QuestSO questData;
    public QuestSO QuestData => questData;
    public List<PlanetTile> Tiles => tiles;
    public List<QuestPing> QuestPing => tiles.Select(t => t.QuestPing).ToList();
    public QuestState QuestState { get; private set; }

    public bool IsActive { get; private set; }
    public bool CanRunQuest => QuestState == QuestState.InProgress && IsActive;
    public bool IsComplete => tiles.All(t => !t.QuestPing.IsEnable);

    public virtual void RunQuest(int num)
    {
        QuestPing[num].DisablePing();

        if(IsComplete)
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
        for(int i = 0; i < tiles.Count; i++)
        {
            if (tiles != null && tiles[i].QuestPing != null)
                QuestPing[i].EnablePing();
        }
        
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
        => QuestState = state;
}