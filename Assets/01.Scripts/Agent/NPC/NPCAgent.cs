using EditorAttributes;
using JMT.Agent.State;
using JMT.Building.Component;
using JMT.DayTime;
using JMT.UISystem;
using System;
using UnityEditor;
using UnityEngine;

namespace JMT.Agent.NPC
{
    public class NPCAgent : AgentAI<NPCState>
    {
        [field: SerializeField] public NPCWorkData WorkData { get; private set; }
        [field: SerializeField] public NPCWork WorkCompo { get; private set; }
        [field: SerializeField] public NPCStat StatCompo { get; private set; }
        
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
            StatCompo = GetComponent<NPCStat>();
            npcStatUI = GetComponent<NPCStatUI>();
            StatCompo?.Initialize(this);
            base.Awake();
            
            
            OnTypeChanged += HandleTypeChanged;
        }

        private void Start()
        {
            WorkData?.Initialize(this);
            WorkCompo?.Initialize(this);
            ClothCompo?.Initialize(this);
            
            StatCompo?.AddListener<Action>(NPCStatEventType.OnDeath, HandleDeath);
            StatCompo?.AddListener<Action>(NPCStatEventType.OnOxygenLowEvent, HandleOxygenLow);
            StatCompo?.AddListener<Action<bool>>(NPCStatEventType.OnHealthWarningEvent, npcStatUI.SetHealthStat);
            StatCompo?.AddListener<Action<bool>>(NPCStatEventType.OnOxygenWarningEvent, npcStatUI.SetOxygenStat);
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += HandleNightEvent;
            
            StateMachineCompo.InitAllState(this);
            StateMachineCompo.ChangeState(NPCState.Idle);
        }
        
        protected void OnDestroy()
        {
            OnTypeChanged -= HandleTypeChanged;
            StatCompo?.RemoveListener<Action>(NPCStatEventType.OnDeath, HandleDeath);
            StatCompo?.RemoveListener<Action>(NPCStatEventType.OnOxygenLowEvent, HandleOxygenLow);
            StatCompo?.RemoveListener<Action<bool>>(NPCStatEventType.OnHealthWarningEvent, npcStatUI.SetHealthStat);
            StatCompo?.RemoveListener<Action<bool>>(NPCStatEventType.OnOxygenWarningEvent, npcStatUI.SetOxygenStat);
            if (GameUIManager.Instance != null)
                GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= HandleNightEvent;
        }

        private void HandleNightEvent(DaytimeType daytimeType)
        {
            if (daytimeType == DaytimeType.Night)
            {
                WorkCompo.CurrentWorkingBuilding?.GetBuildingComponent<BuildingWorker>().StopWork();
                StateMachineCompo.ChangeState(NPCState.Night);
            }
        }

        private void HandleOxygenLow()
        {
            WorkCompo.CurrentWorkingBuilding?.GetBuildingComponent<BuildingWorker>().StopWork();
            StateMachineCompo.ChangeState(NPCState.Dead);
        }

        private void HandleDeath()
        {
            Debug.Log("Dead");
            WorkCompo.CurrentWorkingBuilding?.GetBuildingComponent<BuildingWorker>().StopWork();
            StateMachineCompo.ChangeState(NPCState.Dead, true);
        }
        
        public void RegisterAgent(NPCAgent agent)
        {
            AgentManager.Instance.RegisterAgent(agent);
        }
        
        public void UnregisterAgent(NPCAgent agent)
        {
            AgentManager.Instance.UnregisterAgent(agent);
        }
        
        private void HandleTypeChanged(AgentType type)
        {
            if (type == AgentType.Base)
            {
                RegisterAgent(this);
            }
            else
            {
                UnregisterAgent(this);
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
        
        public void KillNPC()
        {
            StatCompo.HealthCompo.TakeDamage(1000, false);
        }

        protected override void Update()
        {
            base.Update();
#if UNITY_EDITOR
            if (Selection.activeGameObject == gameObject)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    KillNPC();
                }
            }
#endif
        }
    }
}
