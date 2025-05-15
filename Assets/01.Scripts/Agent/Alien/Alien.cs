using JMT.Core.Tool;
using System;
using System.Collections;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT.Agent.Alien
{
    public class Alien : Unity.MLAgents.Agent
    {
        [field: SerializeField] public AgentMovement Movement { get; private set; }
        [field: SerializeField] public Attacker Attacker { get; private set; }
        [field: SerializeField] public AgentHealth Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 1.5f;

        private bool _isAttackSuccess;
        private Transform _playerTransform;
        private Coroutine _moveRoutine;

        private void Start()
        {
            _playerTransform = AgentManager.Instance.Player.transform;
            Health.OnDeath += HandleDeath;
        }

        private void OnDestroy()
        {
            if (Health != null)
                Health.OnDeath -= HandleDeath;

            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);
        }

        private void HandleDeath()
        {
            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            SetReward(-1f);
            EndEpisode();
        }

        public override void OnEpisodeBegin()
        {
            _isAttackSuccess = false;
            Health.InitStat();

            int spawnIndex = Random.Range(0, WaveSystem.Instance.SpawnPoints.Count);
            transform.position = WaveSystem.Instance.SpawnPoints[spawnIndex].transform.position;

            StopAllCoroutines();
            _moveRoutine = StartCoroutine(FollowPlayerContinuously());
        }

        private IEnumerator FollowPlayerContinuously()
        {
            while (!_isAttackSuccess)
            {
                Movement.Move(_playerTransform.position, Speed);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            Vector3 toPlayer = (_playerTransform.position - transform.position).normalized;

            sensor.AddObservation(transform.position);                    // 3
            sensor.AddObservation(transform.rotation.eulerAngles / 360f); // 3
            sensor.AddObservation(_playerTransform.position);            // 3
            sensor.AddObservation(toPlayer);                             // 3

            // 총 12개
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            // 플레이어 방향 바라보기
            Vector3 dirToPlayer = _playerTransform.position - transform.position;
            if (dirToPlayer.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
            }

            float distance = Vector3.Distance(transform.position, _playerTransform.position);

            if (distance < 4f)
            {
                AddReward(+0.2f); // 접근 보상
            }

            TryAttack(distance);

            AddReward(-0.001f); // 시간 패널티
        }

        private void TryAttack(float distance)
        {
            if (_isAttackSuccess) return;

            if (distance < 2f)
            {
                if (Attacker.Attack())
                {
                    _isAttackSuccess = true;
                    SetReward(+1f); // 공격 성공
                }
                else
                {
                    SetReward(-0.5f); // 공격 실패
                }
            }
        }

        private void Update()
        {
            if (!Health.IsDead)
            {
                AddReward(0.001f); // 생존 중 보상 (너무 크지 않게 조절됨)
            }
        }
    }
}
