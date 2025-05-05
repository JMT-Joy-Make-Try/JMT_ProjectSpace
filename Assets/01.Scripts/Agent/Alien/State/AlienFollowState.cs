using JMT.Agent.Alien;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT.Agent.State
{
    public class AlienFollowState : State<AlienState>
    {
        private bool _isAmbush;
        [SerializeField] private float ambushRange = 10f;
        [SerializeField] private LayerMask fogLayerMask;
        private Alien.Alien _alien;
        private Vector3 _targetPosition;
        private bool _wasFollowingTarget = false;

        private static readonly Collider[] _overlapResults = new Collider[10];

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            base.EnterState();
            _isAmbush = Random.Range(0f, 1f) < 0.5f;

            if (_isAmbush)
            {
                _isAmbush = false;
                _stateMachine.ChangeState(AlienState.Idle);
                return;
            }

            RandomMove();
        }


        public override void UpdateState()
        {
            var target = _alien.TargetFinder.Target;

            if (_alien.transform.position.IsNear(_targetPosition, 0.5f))
            {
                if (_isAmbush && !IsPositionInFog(_alien.transform.position))
                {
                    _isAmbush = false;
                    //_stateMachine.ChangeState(AlienState.Idle);
                    return;
                }

                if (!IsPositionInFog(_alien.transform.position) || target == null)
                {
                    _stateMachine.ChangeState(AlienState.Idle);
                    return;
                }

                TargetMove(target.position);
            }

            Agent.MovementCompo.Move(_targetPosition, _alien.MoveSpeed);
        }



        private void TargetMove(Vector3 targetPosition)
        {
            if (IsPositionInFog(targetPosition))
            {
                _targetPosition = targetPosition;
                if (_alien.transform.position.IsNear(targetPosition, 10f))
                {
                    _stateMachine.ChangeState((AlienState)Random.Range(2, 5));
                }
            }
            else
            {
                _targetPosition = _alien.transform.position.GetRandomNearestPosition(10);
            }
        }

        private void RandomMove()
        {
            if (_isAmbush)
            {
                _targetPosition = _alien.transform.position;
                return;
            }

            Vector3 center = Vector3.zero;

            if (IsPositionInFog(center))
            {
                _targetPosition = center;
                return;
            }

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
    }
}