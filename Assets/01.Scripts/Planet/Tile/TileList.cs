using JMT.Building;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileList : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetTile> Tiles { get; private set; } = new List<PlanetTile>();
        [SerializeField] private VillageBuilding _villageBuilding;
        [SerializeField] private int _maxVillageCount;
        [SerializeField] private FogTier _fogTier;
        private GameObject glowObject;
        public LineRenderer LineRenderer { get; private set; }
        public FogTier FogTier => _fogTier;

        private void Awake()
        {
            Tiles = GetComponentsInChildren<PlanetTile>().ToList();
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.enabled = false;
        }

        public void SetTile(TileType tileType, Color color)
        {
            foreach (var tile in Tiles)
            {
                tile.TileType = tileType;
                tile.Renderer.material.SetColor("_BaseColor", color);
            }
            StartCoroutine(SpawnCo());
        }

        private IEnumerator SpawnCo()
        {
            int spawnedCount = 0;
            int attemptLimit = 1000;
            int attempts = 0;

            while (spawnedCount < _maxVillageCount && attempts < attemptLimit)
            {
                attempts++;

                int idx = UnityEngine.Random.Range(0, Tiles.Count);
                PlanetTile tile = Tiles[idx];

                if (tile != null && tile.TryGetInteraction(out NoneInteraction interaction))
                {
                    Instantiate(_villageBuilding, tile.TileInteraction.transform);
                    tile.RemoveInteraction();
                    tile.AddInteraction<VillageInteraction>();
                    spawnedCount++;

                    yield return null; // 다음 스폰까지 한 프레임 대기
                }
            }

            if (spawnedCount < _maxVillageCount)
            {
                Debug.LogWarning($"SpawnCo: Only spawned {spawnedCount}/{_maxVillageCount} villages due to limited valid tiles.");
            }
        }

    }
    
    public enum FogTier
    {
        None,
        One,
        Two,
        Three
    }
}