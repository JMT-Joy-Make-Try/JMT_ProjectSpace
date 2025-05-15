using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Agent.State;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingNPC : MonoBehaviour, IBuildingComponent
    {
        public event Action OnChangeNpcEvent;

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
            agent.WorkCompo.SetBuilding(Building);
            agent.SetAgentType(_agentType);
            OnChangeNpcEvent?.Invoke();
            Debug.Log(agent + " added to " + Building.name);
        }
        
        public virtual void RemoveNpc()
        {
            _currentNpc[0].SetBase();
            _currentNpc.Remove(_currentNpc[0]);
            OnChangeNpcEvent?.Invoke();
            if (_currentNpc.Count == 0)
            {
                Building.SetWorking(false);
            }
        }

        public void RemoveAllNpc()
        {
            for (int i = 0; i < _currentNpc.Count; i++)
            {
                _currentNpc[i].SetBase();
            }

            _currentNpc.Clear();
            OnChangeNpcEvent?.Invoke();
            Building.SetWorking(false);
        }
    }
}