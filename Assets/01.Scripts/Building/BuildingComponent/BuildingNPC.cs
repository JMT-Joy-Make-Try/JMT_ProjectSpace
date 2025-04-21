using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Agent.State;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingNPC : MonoBehaviour, IBuildingComponent
    {
        [Header("NPCSetting")] [SerializeField]
        public List<NPCAgent> _currentNpc;
        [SerializeField] protected AgentType _agentType;
        
        
        public BuildingBase Building { get; private set; }
        [field: SerializeField] public Transform WorkPosition { get; protected set; }
        
        public void Init(BuildingBase building)
        {
            Building = building;
            _currentNpc = new List<NPCAgent>();
        }
        
        public virtual void AddNpc(NPCAgent agent)
        {
            _currentNpc.Add(agent);
            agent.SetBuilding(Building);
            agent.SetAgentType(_agentType);
        }
        
        public virtual void RemoveNpc()
        {
            _currentNpc[0].SetAgentType(AgentType.Base);
            _currentNpc[0].ChangeCloth(AgentType.Base);
            _currentNpc[0].SetBuilding(null);
            _currentNpc[0].StateMachineCompo.ChangeState(NPCState.Move);
            _currentNpc.Remove(_currentNpc[0]);
            if (_currentNpc.Count == 0)
            {
                Building.SetWorking(false);
            }
        }
    }
}