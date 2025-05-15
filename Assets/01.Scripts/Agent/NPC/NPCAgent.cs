using JMT.Agent.State;
using JMT.UISystem;
using System;
using UnityEngine;

namespace JMT.Agent.NPC
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [field: SerializeField] public NPCOxygen OxygenCompo { get; private set; }
        [field: SerializeField] public NPCWorkData WorkData { get; private set; }
        [field: SerializeField] public NPCWork WorkCompo { get; private set; }
        [field: SerializeField] public NPCHealth Health { get; private set; }
        
        
        [Header("Working")]
        
        
        [field:SerializeField] public AgentType AgentType { get; private set; }
        private NPCStatUI npcStatUI;
        
        public event Action<AgentType> OnTypeChanged;
        
        
        
        public void SetAgentType(AgentType agentType)
        {
            AgentType = agentType;
            OnTypeChanged?.Invoke(agentType);
        }

        public override void Init()
        {
            base.Init();
            StateMachineCompo.ChangeState(NPCState.Idle);
        }

        protected override void Awake()
        {
            base.Awake();
            OxygenCompo = GetComponent<NPCOxygen>();
            npcStatUI = GetComponent<NPCStatUI>();
            Health = HealthCompo as NPCHealth;
            StateMachineCompo.ChangeState(NPCState.Idle);
            OnTypeChanged += HandleTypeChanged;
        }

        private void Start()
        {
            WorkData.Initialize(this);
            WorkCompo.Initialize(this);
            Health.Initialize(this);
            ClothCompo.Initialize(this);
            
            HealthCompo.OnDeath += HandleDeath;
            Health.OnHealthWarningEvent += npcStatUI.SetHealthStat;
            OxygenCompo.OnOxygenLowEvent += HandleOxygenLow;
            OxygenCompo.OnOxygenWarningEvent += npcStatUI.SetOxygenStat;
        }
        
        protected void OnDestroy()
        {
            OnTypeChanged -= HandleTypeChanged;
            HealthCompo.OnDeath -= HandleDeath;
            OxygenCompo.OnOxygenLowEvent -= HandleOxygenLow;
            Health.OnHealthWarningEvent -= npcStatUI.SetHealthStat;
            OxygenCompo.OnOxygenWarningEvent -= npcStatUI.SetOxygenStat;
        }

        private void HandleOxygenLow()
        {
            WorkCompo.CurrentWorkingBuilding?.StopWork();
            StateMachineCompo.ChangeState(NPCState.Dead);
        }

        private void HandleDeath()
        {
            Debug.Log("Dead");
            StateMachineCompo.ChangeState(NPCState.Dead, true);
        }
        
        private void HandleTypeChanged(AgentType type)
        {
            if (type == AgentType.Base)
            {
                AgentManager.Instance.RegisterAgent(this);
            }
            else
            {
                AgentManager.Instance.UnregisterAgent(this);
            }
            
        }
        

        public void SetAnimator(Animator animator)
        {
            AnimatorCompo = animator;
        }

        public void SetBase()
        {
            SetAgentType(AgentType.Base);
            ClothCompo.ChangeCloth(AgentType.Base);
            WorkCompo.SetBuilding(null);
            StateMachineCompo.ChangeState(NPCState.Move);
        }
    }
}
