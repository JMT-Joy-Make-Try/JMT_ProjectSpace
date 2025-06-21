using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.UISystem.Interact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileManager : MonoSingleton<TileManager>
    {
        [SerializeField] private PlanetTile _planetTile;
        [SerializeField] private List<PlanetTile> _planetTiles = new List<PlanetTile>();
        public PlanetTile CurrentTile
        {
            get => _planetTile;
            set
            {
                if (_planetTile != null)
                    _planetTile.EdgeEnable(false);
                _planetTile = value;
                _planetTile.EdgeEnable(true);
            }
        }
        public Field.Field FieldPrefab;
        
        private Dictionary<Vector2Int, PlanetTile> _tileList = new();
        
        public PlanetTile GetTile(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);
            if (_tileList.TryGetValue(position, out var tile))
            {
                return tile;
            }
            return null;
        }
        
        public void RegisterTile(int x, int y, PlanetTile tile)
        {
            Vector2Int position = new Vector2Int(x, y);
            if (!_tileList.ContainsKey(position))
            {
                _tileList[position] = tile;
            }
            else
            {
                Debug.LogWarning($"Tile at position {position} is already registered.");
            }
        }

        public List<PlanetTile> Get2By2TilesInAnyDirection(PlanetTile curTile, Vector2 forwardDir)
        {
            if (curTile == null) return null;
        
            // 타일 간격
            float tileSize = 15f;
        
            // 방향 정규화
            Vector2 dir = forwardDir.normalized;
        
            // 수직 방향 구하기 (좌측 90도 회전)
            Vector2 perp = new Vector2(-dir.y, dir.x);
        
            // 2x2 정사각형 네 꼭짓점 오프셋
            Vector2[] offsets = new Vector2[]
            {
                Vector2.zero,
                dir * tileSize,
                perp * tileSize,
                dir * tileSize + perp * tileSize
            };
        
            var tiles = new List<PlanetTile>();
            int x0 = curTile.Position.x;
            int y0 = curTile.Position.y;
        
            foreach (var offset in offsets)
            {
                // 오프셋을 반올림하여 정수 좌표로 변환
                int x = Mathf.RoundToInt(x0 + offset.x);
                int y = Mathf.RoundToInt(y0 + offset.y);
        
                if (IsTileValid(x, y, out var tile))
                {
                    tiles.Add(tile);
                }
                else
                {
                    return null;
                }
            }
        
            return tiles;
        }
        
        
        public bool IsTileValid(int x, int y, out PlanetTile tile)
        {
            Vector2Int position = new Vector2Int(x, y);
            if (_tileList.TryGetValue(position, out var tileTmp))
            {
                if (tileTmp.CanBuild())
                {
                    tile = tileTmp;
                    return true;
                }

                Debug.LogWarning($"Tile at position {position} cannot be built on.");
                tile = null;
                return false;
            }
            tile = null;
            return false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                var playerLookDir = AgentManager.Instance.Player.transform.forward;
                _planetTiles = Get2By2TilesInAnyDirection(_planetTile, playerLookDir);
            }
        }


        public TileInteraction GetInteraction() => CurrentTile.TileInteraction;
        public InteractType GetInteractType() => GetInteraction().InteractType;
    }
}
