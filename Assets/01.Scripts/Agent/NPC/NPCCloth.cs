using System;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCCloth : AgentCloth<AgentType>
    {
        private void Start()
        {
            Init(AgentType.Base);
        }
    }
}