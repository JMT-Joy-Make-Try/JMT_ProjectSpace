using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnPoints = new();
        [SerializeField] private GameObject enemy;

        private Coroutine spawnCoroutine;
        private int enemyCount = 1;

        private void Awake()
        {
            DaySystem.Instance.OnDayEvent += StopSpawn;
            DaySystem.Instance.OnNightEvent += StartSpawn;
        }

        public void StartSpawn()
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine(0.5f));
        }

        public void StopSpawn()
        {
            StopCoroutine(spawnCoroutine);
        }

        private IEnumerator SpawnCoroutine(float coolTime)
        {
            var waitTime = new WaitForSeconds(coolTime);
            for(int i = 0; i < enemyCount; i++)
            {
                yield return waitTime;
                int randomValue = Random.Range(0, spawnPoints.Count);
                Instantiate(enemy, spawnPoints[randomValue].transform.position, Quaternion.identity);
            }
            enemyCount += 5;
            yield return null;
        }
    }
}
