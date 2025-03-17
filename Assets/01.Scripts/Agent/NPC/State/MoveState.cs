using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.State
{
    public class MoveState : State<NPCState>
    {
        public override void EnterState()
        {
            base.EnterState();
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            NPCAgent agent = _agent as NPCAgent;
            while (true)
            {
                if (agent.AgentType == AgentType.Base)
                {
                    _agent.MovementCompo.Move(new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f)), agent.MoveSpeed);
                }
                else
                {
                    _agent.MovementCompo.Move(agent.CurrentWorkingPlanetTile.transform.position, agent.MoveSpeed,
                        ChangeState);
                }

                yield return new WaitUntil(() => !_agent.MovementCompo.IsMoving);
            }
        }

        private void ChangeState()
        {
            StartCoroutine(ChangeCoroutine());
        }

        private IEnumerator ChangeCoroutine()
        {
            yield return new WaitUntil(() => !_agent.MovementCompo.IsMoving);
            _agent.StateMachineCompo.ChangeState(NPCState.Work);
        }
    }
}