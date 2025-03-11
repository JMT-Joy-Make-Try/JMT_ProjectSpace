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
       
        protected virtual void Awake()
        {
            StateMachineCompo = gameObject.GetComponentOrAdd<StateMachine<T>>();
            AnimatorCompo = gameObject.GetComponentOrAdd<Animator>();
            MovementCompo = gameObject.GetComponentOrAdd<AgentMovement>();
            
            StateMachineCompo.InitAllState(this);
            //stateMachine.ChangeState(기본 State 넣기);
        }

        protected virtual void Update()
        {
            StateMachineCompo.UpdateState();
        }
    }
}
