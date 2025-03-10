using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentAI : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;

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
