using System;
using AYellowpaper.SerializedCollections;
using JMT.Core.Tool;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentAI<T> : MonoBehaviour where T : Enum
    {
        [field:SerializeField] public StateMachine<T> StateMachineCompo { get; private set; }
        [field:SerializeField] public Animator AnimatorCompo { get; private set; }
        [field:SerializeField] public AgentMovement MovementCompo { get; private set; }
        [field:SerializeField] public AgentCloth ClothCompo { get; private set; }
        [field:SerializeField] public AnimationEndTrigger AnimationEndTrigger { get; private set; }
        
        public bool IsDead { get; private set; }
       
        protected virtual void Awake()
        {
            StateMachineCompo = gameObject.GetComponentOrAdd<StateMachine<T>>();
            AnimatorCompo = gameObject.GetComponentOrAdd<Animator>();
            MovementCompo = gameObject.GetComponentOrAdd<AgentMovement>();
            ClothCompo = gameObject.GetComponentOrAdd<AgentCloth>();
            AnimationEndTrigger = gameObject.GetComponentOrAdd<AnimationEndTrigger>();
            
            StateMachineCompo.InitAllState(this);
            //AnimationEndTrigger.OnAnimationEnd += StateMachineCompo.CurrentState.OnAnimationEnd;
        }

        protected virtual void Update()
        {
            StateMachineCompo.UpdateState();
        }

        protected virtual void OnDestroy()
        {
            //AnimationEndTrigger.OnAnimationEnd -= StateMachineCompo.CurrentState.OnAnimationEnd;
        }
    }
}
