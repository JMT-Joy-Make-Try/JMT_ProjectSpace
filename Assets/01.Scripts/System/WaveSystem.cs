using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.DayTime;
using JMT.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnPoints = new();
        [SerializeField] private int _increaseEnemyCount = 5;
        [SerializeField] private int _maxEnemyCount = 10;

        private List<GameObject> _enemies = new();

        private Coroutine spawnCoroutine;
        private int enemyCount = 1;

        private void Awake()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += EnemySpawn;
        }

        private void OnDestroy()
        {
            if (spawnCoroutine != null)
                StopCoroutine(spawnCoroutine);

            if (GameUIManager.Instance.TimeCompo != null)
                GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= EnemySpawn;
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
            if (_enemies.Count >= _maxEnemyCount)
            {
                yield break;
            }
            var waitTime = new WaitForSeconds(coolTime);
            for (int i = 0; i < enemyCount; i++)
            {
                yield return waitTime;
                int randomValue = Random.Range(0, spawnPoints.Count);
                var obj = PoolingManager.Instance.Pop(PoolingType.Enemy_Ailen);
                obj.ObjectPrefab.transform.position = spawnPoints[randomValue].transform.position;
                _enemies.Add(obj.ObjectPrefab);
            }

            enemyCount += _increaseEnemyCount;
            yield return null;
        }
    }
}
