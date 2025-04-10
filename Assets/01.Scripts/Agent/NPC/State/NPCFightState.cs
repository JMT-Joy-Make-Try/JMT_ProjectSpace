using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCFightState : State<NPCState>
    {
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
        }

        public override void EnterState()
        {
            base.EnterState();
            /// 몰라
        }

        public override void UpdateState()
        {
            base.UpdateState();
            /// 타겟이 없으면 적을 찾아
            /// 타켓이 생기면 따라가
            /// 타켓한테 도착하면 때려
            /// 그리고 쿨타임
            /// 반복
        }

        public override void ExitState()
        {
            /// 퇴사를 할때
            /// 뒤졌을 때
            base.ExitState();
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
        }
    }
}