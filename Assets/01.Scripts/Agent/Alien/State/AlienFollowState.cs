using JMT.Agent.Alien;
using JMT.Planets.Tile;
using System.Collections;
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
        private Coroutine _randomTargetCoroutine;
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

            if (_isAmbush)
            {
                _randomTargetCoroutine = null;
            }
            else
            {
                _targetPosition = Vector3.zero;
                StartCoroutine(RandomTargetPosition());
            }
        }

        public override void UpdateState()
        {
            var target = _alien.TargetFinder.Target;

            if (_isAmbush && (target == null || Vector3.Distance(target.position, Agent.transform.position) > ambushRange))
                return;

            if (target != null && IsPositionInFog(target.position))
            {
                _wasFollowingTarget = true;
                Agent.MovementCompo.Stop(_isAmbush);
                Agent.MovementCompo.Move(target.position, _alien.MoveSpeed);
                if (Agent.MovementCompo.IsNearestTarget(target.position, 100f))
                {
                    Debug.LogError("이동 완료 후 상태 변경");
                    _stateMachine.ChangeState((AlienState)Random.Range(2, 5));
                }
                return;
            }

            if (_isAmbush && _wasFollowingTarget)
            {
                _wasFollowingTarget = false;
                _isAmbush = false;  
                _randomTargetCoroutine = StartCoroutine(RandomTargetPosition());
            }

            Agent.MovementCompo.Move(_targetPosition, _alien.MoveSpeed);
        }

        private IEnumerator RandomTargetPosition()
        {
            var ws = new WaitForSeconds(2f);
            Vector3 center = Vector3.zero;

            while (true)
            {
                Vector3 bestPosition = Vector3.zero;
                float bestDistance = float.MaxValue;
                bool found = false;

                for (int i = 0; i < 10; i++)
                {
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-15f, 15f),
                        0f,
                        Random.Range(-15f, 15f)
                    );
                    Vector3 candidate = Vector3.Lerp(_alien.transform.position, center, 0.3f) + randomOffset;

                    if (IsPositionInFog(candidate))
                    {
                        float distToCenter = Vector3.Distance(candidate, center);
                        if (distToCenter < bestDistance)
                        {
                            bestDistance = distToCenter;
                            bestPosition = candidate;
                            found = true;
                        }
                    }
                }

                if (found)
                {
                    _targetPosition = bestPosition;
                }
                else
                {
                    Debug.Log("중앙 근처에 안개 위치 없음");
                }

                yield return ws;
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
