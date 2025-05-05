using JMT.Agent.Alien;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT.Agent.State
{
    public class AlienFollowState : State<AlienState>
    {
        private Alien.Alien _alien;
        private Vector3 _targetPosition;

        private static readonly Collider[] _overlapResults = new Collider[10];
        private Coroutine _moveCoroutine;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            base.EnterState();

            RandomMove();
            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                var target = _alien.TargetFinder.Target;

                if (target == null)
                {
                    RandomMove();
                    Debug.Log("AlienFollowState: No target found, moving randomly.");
                }
                else
                {
                    TargetMove(target.position);
                    Debug.Log("AlienFollowState: Following target.");
                }


                if (IsPositionInFog(_targetPosition))
                {
                    Agent.MovementCompo.Move(_targetPosition, _alien.MoveSpeed);
                }

                yield return new WaitUntil(() => !Agent.MovementCompo.IsMoving);
            }
        }



        private void TargetMove(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            if (_alien.transform.position.IsNear(targetPosition, 10f))
            {
                _stateMachine.ChangeState((AlienState)Random.Range(2, 5));
            }
        }

        private void RandomMove()
        {
            Vector3 center = Vector3.zero;

            for (int i = 0; i < 10; i++)
            {
                Vector3 toCenter = (center - _alien.transform.position).normalized;
                Vector3 randomOffset = Random.insideUnitSphere * 3f;
                randomOffset.y = 0;

                Vector3 candidate = _alien.transform.position + toCenter * Random.Range(3f, 7f) + randomOffset;

                if (IsPositionInFog(candidate))
                {
                    _targetPosition = candidate;
                    return;
                }
            }

            _targetPosition = _alien.transform.position + (center - _alien.transform.position).normalized * 5f;
        }


        private bool IsPositionInFog(Vector3 position)
        {
            int cnt = Physics.OverlapSphereNonAlloc(position, 1f, _overlapResults);
            for (int i = 0; i < cnt; i++)
            {
                var fog = _overlapResults[i].GetComponent<Fog>();
                if (fog != null && fog.IsFogActive)
                    return true;
            }

            return false;
        }

        public override void ExitState()
        {
            StopCoroutine(_moveCoroutine);
            base.ExitState();
        }
    }
}