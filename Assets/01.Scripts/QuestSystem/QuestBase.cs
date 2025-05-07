using JMT;
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
    }

    public virtual void Enable()
    {
        QuestState = QuestState.InProgress;
        IsActive = true;
        QuestPing.EnablePing();
        Debug.Log("Quest enabled: " + QuestData.questName);
    }

    public void SetState(QuestState state)
        => QuestState = state;
}