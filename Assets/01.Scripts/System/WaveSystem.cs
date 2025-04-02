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
        [SerializeField] private int _increaseEnemyCount = 5;

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
                    if (spawnCoroutine != null)
                        StopCoroutine(spawnCoroutine);
                    break;
                case DaytimeType.Night:
                    spawnCoroutine = StartCoroutine(SpawnCoroutine(0.5f));
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

            enemyCount += _increaseEnemyCount;
            yield return null;
        }
    }
}
