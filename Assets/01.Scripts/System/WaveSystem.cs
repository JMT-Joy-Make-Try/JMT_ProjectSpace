using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.DayTime;
using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT
{
    public class WaveSystem : MonoSingleton<WaveSystem>
    {
        public event Action<List<GameObject>> OnCompleteSpawnEvent;
        public event Action OnClearEvent;
        [SerializeField] private List<GameObject> spawnPoints = new();
        [SerializeField] private int _startEnemyCount = 4;
        [SerializeField] private int _increaseEnemyCount = 1;
        [SerializeField] private int _maxEnemyCount = 10;

        public List<GameObject> Enemies { get; private set; } = new();

        private Coroutine spawnCoroutine;

        protected override void Awake()
        {
            //GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += EnemySpawn;
        }

        private void OnDestroy()
        {
            // if (spawnCoroutine != null)
            //     StopCoroutine(spawnCoroutine);
            // if (GameUIManager.Instance == null) return;
            //
            // if (GameUIManager.Instance.TimeCompo != null)
            //     GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= EnemySpawn;
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

        public void EnemyRemove(GameObject obj)
        {
            Enemies.Remove(obj);
            if(Enemies.Count <= 0)
                OnClearEvent?.Invoke();
        }

        private IEnumerator SpawnCoroutine(float coolTime)
        {
            if (Enemies.Count >= _maxEnemyCount)
            {
                yield break;
            }
            var waitTime = new WaitForSeconds(coolTime);
            for (int i = 0; i < _startEnemyCount; i++)
            {
                yield return waitTime;
                int randomValue = Random.Range(0, spawnPoints.Count);
                var obj = PoolingManager.Instance.Pop(PoolingType.Enemy_Ailen);
                obj.ObjectPrefab.transform.position = spawnPoints[randomValue].transform.position;
                Enemies.Add(obj.ObjectPrefab);
            }
            OnCompleteSpawnEvent?.Invoke(Enemies);
            _startEnemyCount += _increaseEnemyCount;
            yield return null;
        }
    }
}
