using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentAI<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] public StateMachine<T> stateMachine;
        [SerializeField] public Animator animator;
        protected virtual void Awake()
        {
            stateMachine.InitState(this);
            //stateMachine.ChangeState(기본 State 넣기);
        }
        
        protected virtual void Update()
        {
            stateMachine.UpdateState();
        }
    }
}
