using JMT.Agent.NPC;
using System;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCCloth : AgentCloth<AgentType>, INPCComponent
    {
        public NPCAgent Agent { get; private set; }
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
            Init(AgentType.Base);
        }
        
        public void ChangeCloth(AgentType agentType)
        {
            SetCloth(agentType);
            Agent.SetAnimator(CurrentCloth);
            Agent.AnimatorCompo.SetBool(Agent.StateMachineCompo.CurrentState.StateName, true);
        }

        
    }
}