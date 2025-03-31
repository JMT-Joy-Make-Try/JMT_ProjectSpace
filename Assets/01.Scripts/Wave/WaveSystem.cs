using JMT.Agent.Alien;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnPoints = new();
        [SerializeField] private Alien enemy;

        private Coroutine spawnCoroutine;
        private int enemyCount = 1;

        private void Awake()
        {
            DaySystem.Instance.OnChangeDaytimeEvent += EnemySpawn;
        }

        public void EnemySpawn(DaytimeType type)
        {
            switch (type)
            {
                case DaytimeType.Day:
                    spawnCoroutine = StartCoroutine(SpawnCoroutine(0.5f));
                    break;
                case DaytimeType.Night:
                    StopCoroutine(spawnCoroutine);
                    break;
            }
        }

        private IEnumerator SpawnCoroutine(float coolTime)
        {
            var waitTime = new WaitForSeconds(coolTime);
            for(int i = 0; i < enemyCount; i++)
            {
                yield return waitTime;
                int randomValue = Random.Range(0, spawnPoints.Count);
                var obj = PoolingManager.Instance.Pop(PoolingType.Enemy_Ailen);
                obj.ObjectPrefab.transform.position = spawnPoints[randomValue].transform.position;
            }
            enemyCount += 5;
            yield return null;
        }
    }
}
