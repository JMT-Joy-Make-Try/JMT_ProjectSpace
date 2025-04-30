using JMT.Agent.State;
using JMT.Building;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Range = JMT.Core.Tool.Range;

namespace JMT.Agent.NPC
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [field: SerializeField] public NPCOxygen OxygenCompo { get; private set; }
        [Header("Unlock NPC")]
        [field: SerializeField] public NPCData Data { get; set; }
        public bool IsActive { get; private set; }
        
        [Header("Working")]
        [field: SerializeField] public BuildingBase CurrentWorkingBuilding { get; private set; }
        [field: SerializeField] public PlanetTile CurrentWorkingPlanetTile { get; private set; }
        public bool IsWorking { get; private set; }
        

        [Space]
        [SerializeField] private List<Range> _healthRange;
        [field:SerializeField] public int MoveSpeed;
        [field:SerializeField] public int WorkSpeed;
        
        [field:SerializeField] public AgentType AgentType { get; private set; }
        private NPCStatUI npcStatUI;
        
        public event Action<AgentType> OnTypeChanged;
        public event Action<bool> OnHealthWarningEvent;
        
        
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
            OxygenCompo = GetComponent<NPCOxygen>();
            npcStatUI = GetComponent<NPCStatUI>();

            OnTypeChanged += HandleTypeChanged;
            OnDeath += HandleDeath;
            OxygenCompo.OnOxygenLowEvent += HandleOxygenLow;
            OnHealthWarningEvent += npcStatUI.SetHealthStat;
            OxygenCompo.OnOxygenWarningEvent += npcStatUI.SetOxygenStat;

            base.Awake();
            
            StateMachineCompo.ChangeState(NPCState.Idle);


            ActiveAgent();
        }

        private void HandleOxygenLow()
        {
            CurrentWorkingBuilding?.StopWork();
            StateMachineCompo.ChangeState(NPCState.Dead);
        }

        protected void OnDestroy()
        {
            OnTypeChanged -= HandleTypeChanged;
            OnDeath -= HandleDeath;
            OxygenCompo.OnOxygenLowEvent -= HandleOxygenLow;
            OnHealthWarningEvent -= npcStatUI.SetHealthStat;
            OxygenCompo.OnOxygenWarningEvent -= npcStatUI.SetOxygenStat;
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

        
        private void ActiveAgent()
        {
            IsActive = true;
        }
        
        protected void SetSpeed()
        {
            WorkSpeed = GetPercent(_curHealth);
            MoveSpeed = MathExtension.GetPercentageValue(MoveSpeed, GetPercent(_curHealth));

            if (WorkSpeed <= 0)
            {
                ChangeCloth(AgentType.Patient);
            }
        }

        public void ChangeCloth(AgentType agentType)
        {
            ClothCompo.SetCloth(agentType);
            SetAnimator(ClothCompo.CurrentCloth);
            AnimatorCompo.SetBool(StateMachineCompo.CurrentState.StateName, true);
        }

        protected int GetPercent(float health)
        {
            int healthPercent = Mathf.RoundToInt(health * 100 / Health);
            int rangeValue = healthPercent.GetRangeValue(_healthRange);
            var findRange = _healthRange.Find(s => s.ReturnValue == rangeValue);
            var rangeIndex = _healthRange.IndexOf(findRange);

            if (rangeIndex == 2)
                OnHealthWarningEvent?.Invoke(true);
            else
                OnHealthWarningEvent?.Invoke(true);
            return rangeValue;
        }

        public override void TakeDamage(int damage, bool isHeal)
        {
            base.TakeDamage(damage, isHeal);
            int healthPercent = Mathf.RoundToInt(_curHealth * 100 / Health);
            if (healthPercent < 40)
                OnHealthWarningEvent?.Invoke(true);
            else
                OnHealthWarningEvent?.Invoke(true);
        }

        public void SetBuilding(BuildingBase building)
        {
            if (building == null) return;
            CurrentWorkingBuilding = building;
        }

        public void SetAnimator(Animator animator)
        {
            AnimatorCompo = animator;
        }
    }
}
