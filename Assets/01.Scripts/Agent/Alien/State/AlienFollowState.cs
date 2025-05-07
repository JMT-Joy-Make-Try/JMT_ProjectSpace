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
        private bool _isAmbush = false;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            base.EnterState();
            _isAmbush = Random.value > 0.5f;

            RandomMove();
            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                var target = _alien.TargetFinder.Target;

                if (target != null)
                {
                    if (_alien.transform.position.IsNear(target.position, 10f))
                    {
                        _alien.TargetFinder.Target = null;
                        _stateMachine.ChangeState((AlienState)Random.Range(2, 5));
                    }
                }

                if (_isAmbush)
                {
                    Agent.MovementCompo.Stop(true);
                }
                else
                {
                    Agent.MovementCompo.Stop(false);
                }

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
                Agent.MovementCompo.Move(_targetPosition, _alien.MoveSpeed);

                yield return new WaitUntil(() => !Agent.MovementCompo.IsMoving);
            }
        }



        private void TargetMove(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            
        }

        private void RandomMove()
        {
            _targetPosition = Vector3.zero;
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
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            base.ExitState();
        }
    }
}