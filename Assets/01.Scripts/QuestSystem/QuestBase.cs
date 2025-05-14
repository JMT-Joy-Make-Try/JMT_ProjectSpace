using JMT;
using JMT.Agent;
using JMT.Planets.Tile;
using JMT.QuestSystem;
using UnityEngine;

public class QuestBase : MonoBehaviour, IQuestTarget
{
    [SerializeField] protected PlanetTile tile;
    [field: SerializeField] public QuestSO QuestData { get; private set; }
    public PlanetTile Tile => tile;
    public QuestState QuestState { get; private set; }
    public QuestPing QuestPing => tile.QuestPing;
    public bool IsActive { get; private set; }
    public bool CanRunQuest => QuestState == QuestState.InProgress && IsActive;

    protected bool isComplete;

    public virtual void RunQuest()
    {
        QuestPing.DisablePing();
        QuestManager.Instance.CompleteQuest(QuestData);
        GetReward(QuestData);
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
        if (tile != null && tile.QuestPing != null)
            QuestPing.EnablePing();
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
        => QuestState = state;
}