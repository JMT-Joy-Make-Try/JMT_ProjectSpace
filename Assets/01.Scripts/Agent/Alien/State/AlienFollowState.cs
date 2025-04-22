using JMT.Agent.Alien;
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
    private Coroutine _randomTargetCoroutine;

    private static readonly Collider[] _overlapResults = new Collider[10];
    private static readonly WaitForSeconds _wait1Sec = new WaitForSeconds(1f);
    private static readonly WaitForSeconds _wait2Sec = new WaitForSeconds(2f);

    private bool _wasFollowingTarget = false;

    public override void Initialize(AgentAI<AlienState> agent, string stateName)
    {
        base.Initialize(agent, stateName);
        _alien = (Alien.Alien)agent;
    }

    public override void EnterState()
    {
        Agent.MovementCompo.Stop(false);
        base.EnterState();
        _randomTargetCoroutine = StartCoroutine(RandomTargetPosition());
    }

    public override void ExitState()
    {
        if (_randomTargetCoroutine != null)
        {
            StopCoroutine(_randomTargetCoroutine);
        }
    }

    public override void UpdateState()
    {
        var target = _alien.TargetFinder.Target;

        if (target != null && IsPositionInFog(target.position))
        {
            _wasFollowingTarget = true;
            Agent.MovementCompo.Move(target.position, _alien.MoveSpeed);

            if (Vector3.Distance(target.position, Agent.transform.position) < _alien.Attacker.AttackRange)
            {
                Agent.MovementCompo.Stop(true);
                int attackState = Random.Range(2, 5);
                Agent.StateMachineCompo.ChangeState((AlienState)attackState);
            }

            return;
        }

        if (_wasFollowingTarget)
        {
            _wasFollowingTarget = false;

            _alien.TargetFinder.Target = null;

            if (_randomTargetCoroutine != null)
                StopCoroutine(_randomTargetCoroutine);

            _randomTargetCoroutine = StartCoroutine(RandomTargetPosition());
        }

        Agent.MovementCompo.Move(_targetPosition, _alien.MoveSpeed);
    }


    private IEnumerator RandomTargetPosition()
    {
        while (true)
        {
            if (_alien.TargetFinder.Target != null && IsPositionInFog(_alien.TargetFinder.Target.position))
            {
                yield break; 
            }

            bool foundValidPosition = false;

            for (int i = 0; i < 10; i++)
            {
                float range = 20f;
                Vector3 potentialPosition = new Vector3(
                    Random.Range(_alien.transform.position.x - range, _alien.transform.position.x + range),
                    _alien.transform.position.y,
                    Random.Range(_alien.transform.position.z - range, _alien.transform.position.z + range)
                );

                if (IsPositionInFog(potentialPosition))
                {
                    _targetPosition = potentialPosition;
                    foundValidPosition = true;
                    break;
                }
            }

            if (!foundValidPosition)
            {
                Debug.Log("Fog가 있는 랜덤 위치를 찾지 못했습니다.");
            }

            yield return new WaitForSeconds(2f); 
        }
    }



    private bool IsPositionInFog(Vector3 position)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(position, 1f, _overlapResults);
        for (int i = 0; i < hitCount; i++)
        {
            var fog = _overlapResults[i].GetComponent<Fog>();
            if (fog != null && fog.IsFogActive)
            {
                return true;
            }
        }
        return false;
    }
}

}
