using System.Collections;
using Unity.MLAgents;
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
        [field: SerializeField] public float Speed { get; private set; } = 1.5f;

        private bool _isAttackSuccess;
        private bool _hasApproachedPlayer;
        private Transform _playerTransform;

        public override void OnEpisodeBegin()
        {
            int spawnIndex = Random.Range(0, WaveSystem.Instance.SpawnPoints.Count);
            transform.position = WaveSystem.Instance.SpawnPoints[spawnIndex].transform.position;

            _playerTransform = AgentManager.Instance.Player.transform;

            _isAttackSuccess = false;
            _hasApproachedPlayer = false;

            Movement.Stop(false); // 이동 가능 상태로 설정

            StartCoroutine(TargetNotFound());
        }

        private IEnumerator TargetNotFound()
        {
            yield return new WaitForSeconds(8f);

            if (!_isAttackSuccess)
            {
                if (_hasApproachedPlayer)
                    SetReward(-0.3f);
                else
                    SetReward(-1f);

                EndEpisode();
            }
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            Vector3 toPlayer = _playerTransform.position - transform.position;

            sensor.AddObservation(transform.position);             // 3
            sensor.AddObservation(transform.forward);              // 3
            sensor.AddObservation(_playerTransform.position);      // 3
            sensor.AddObservation(toPlayer.normalized);            // 3
            sensor.AddObservation(toPlayer.magnitude);             // 1
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            float x = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
            float z = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);

            Vector3 moveDir = new Vector3(x, 0, z);
            Vector3 targetPos = transform.position + moveDir.normalized * 2f; // 너무 멀리 잡지 않도록
            Movement.Move(targetPos, Speed);

            float distance = Vector3.Distance(transform.position, _playerTransform.position);

            if (distance < 3.5f && !_hasApproachedPlayer)
            {
                _hasApproachedPlayer = true;
                AddReward(+0.2f);
            }

            if (distance < 2f && !_isAttackSuccess)
            {
                if (Attacker.Attack())
                {
                    _isAttackSuccess = true;
                    SetReward(+1.0f);
                    EndEpisode();
                }
            }

            AddReward(-0.001f); // 시간 패널티
        }

        private void FixedUpdate()
        {
            float dist = Vector3.Distance(transform.position, _playerTransform.position);
            float distanceReward = Mathf.Clamp01(1f - dist / 10f);
            AddReward(0.05f * distanceReward);
        }
    }
}
