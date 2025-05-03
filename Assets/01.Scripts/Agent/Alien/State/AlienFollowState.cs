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
            Agent.MovementCompo.Stop(_isAmbush);
        }

        public override void UpdateState()
        {
            var target = _alien.TargetFinder.Target;

            if (target == null)
            {
                RandomMove();
            }
            else
            {
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
            }
            else
            {
                _targetPosition = _alien.transform.position.GetRandomNearestPosition(ambushRange);
            }
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
