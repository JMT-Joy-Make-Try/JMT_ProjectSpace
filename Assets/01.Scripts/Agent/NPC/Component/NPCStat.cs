using JMT.Agent.NPC;
using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCStat : MonoBehaviour, INPCComponent
    {
        [field: SerializeField] public NPCStatSO StatSO { get; private set; }
        public NPCAgent Agent { get; private set; }

        public NPCOxygen OxygenCompo { get; private set; }
        public NPCHealth HealthCompo { get; private set; }
        
        [field:SerializeField] public int MoveSpeed;
        [field:SerializeField] public int WorkSpeed;

        private float _satisfaction; // NPC의 만족도
        
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
            OxygenCompo = agent.GetComponent<NPCOxygen>();
            HealthCompo = agent.GetComponent<NPCHealth>();
            
            HealthCompo?.Initialize(Agent);
            OxygenCompo?.Initialize(Agent);
        }
        
        #region Actions
        public void AddListener<T>(NPCStatEventType eventType, T action) where T : Delegate
        {
            switch (eventType)
            {
                case NPCStatEventType.OnDeath:
                    HealthCompo.OnDeath += action as Action;
                    break;
                case NPCStatEventType.OnOxygenLowEvent:
                    OxygenCompo.OnOxygenLowEvent += action as Action;
                    break;
                case NPCStatEventType.OnHealthWarningEvent:
                    HealthCompo.OnHealthWarningEvent += action as Action<bool>;
                    break;
                case NPCStatEventType.OnOxygenWarningEvent:
                    OxygenCompo.OnOxygenWarningEvent += action as Action<bool>;
                    break;
            }
        }
        
        public void RemoveListener<T>(NPCStatEventType eventType, T action) where T : Delegate
        {
            switch (eventType)
            {
                case NPCStatEventType.OnDeath:
                    HealthCompo.OnDeath -= action as Action;
                    break;
                case NPCStatEventType.OnOxygenLowEvent:
                    OxygenCompo.OnOxygenLowEvent -= action as Action;
                    break;
                case NPCStatEventType.OnHealthWarningEvent:
                    HealthCompo.OnHealthWarningEvent -= action as Action<bool>;
                    break;
                case NPCStatEventType.OnOxygenWarningEvent:
                    OxygenCompo.OnOxygenWarningEvent -= action as Action<bool>;
                    break;
            }
        }
        #endregion
    }
    
    public enum NPCStatEventType
    {
        OnDeath,
        OnHealthWarningEvent,
        OnOxygenLowEvent,
        OnOxygenWarningEvent
    }
}