using System;
using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.UISystem;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentAI<T> : MonoBehaviour, IPoolable where T : Enum
    {
        [field:SerializeField] public StateMachine<T> StateMachineCompo { get; private set; }
        [field:SerializeField] public Animator AnimatorCompo { get; protected set; }
        [field:SerializeField] public AgentMovement MovementCompo { get; private set; }
        [field:SerializeField] public NPCCloth ClothCompo { get; private set; }
        [field:SerializeField] public AnimationEndTrigger AnimationEndTrigger { get; private set; }
        [field:SerializeField] public AgentHealth HealthCompo { get; private set; }
        
        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;

        protected virtual void Awake()
        {
            StateMachineCompo = gameObject.GetComponentOrAdd<StateMachine<T>>();
            AnimatorCompo = gameObject.GetComponentOrAdd<Animator>();
            MovementCompo = gameObject.GetComponentOrAdd<AgentMovement>();
            ClothCompo = GetComponent<NPCCloth>();
            AnimationEndTrigger = GetComponentInChildren<AnimationEndTrigger>();
            HealthCompo = GetComponent<AgentHealth>();
            
            StateMachineCompo.InitAllState(this);
            Init();
        }

        protected virtual void Update()
        {
            StateMachineCompo.UpdateState();
        }

        public void ResetItem()
        {
            Init();
        }

        public virtual void Init()
        {
            HealthCompo.InitStat();
            MovementCompo.Stop(false);
        }
    }
}
