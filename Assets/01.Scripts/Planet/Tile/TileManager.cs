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
                {
                    _planetTile.EdgeEnable(false);
                    
                }
                
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

            // 방향 정규화 후 4방향 스냅
            Vector2 dir = forwardDir.normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                dir = new Vector2(Mathf.Sign(dir.x), 0);
            else
                dir = new Vector2(0, Mathf.Sign(dir.y));

            // 왼쪽 방향 (dir을 기준으로 좌측 90도 회전)
            Vector2 left = new Vector2(-dir.y, dir.x);

            // 왼위 2x2 타일 좌표들
            Vector2Int[] offsets = new Vector2Int[]
            {
                Vector2Int.zero,                    // 현재 타일
                Vector2Int.RoundToInt(left),        // 왼쪽
                Vector2Int.RoundToInt(dir),         // 위쪽 (플레이어 전방)
                Vector2Int.RoundToInt(left + dir)   // 왼쪽 + 위쪽 = 대각
            };

            int unit = 10; // 타일 간격
            int x0 = curTile.Position.x;
            int y0 = curTile.Position.y;

            var tiles = new List<PlanetTile>();

            foreach (var offset in offsets)
            {
                int x = x0 + offset.x * unit;
                int y = y0 + offset.y * unit;

                if (IsTileValid(x, y, out var tile))
                {
                    tiles.Add(tile);
                }
                else
                {
                    return null; // 하나라도 없으면 실패
                }
            }

            _planetTiles = tiles;
            return tiles;
        }
        
        public List<PlanetTile> Get2By2TilesInAnyDirection(PlanetTile curTile)
        {
            if (curTile == null) return null;
            List<PlanetTile> tiles = new List<PlanetTile>();
            
            tiles.Add(curTile);
            
            Vector2Int[] offset = new Vector2Int[]
            {
                new Vector2Int(0, 10),   // 위
                new Vector2Int(-10, 0),   // 왼쪽
                new Vector2Int(-10, 10),  // 위왼
            };
            
            
            foreach (var off in offset)
            {
                int x = curTile.Position.x + off.x;
                int y = curTile.Position.y + off.y;

                if (IsTileValid(x, y, out var tile))
                {
                    tiles.Add(tile);
                }
                else
                {
                    return null; // 하나라도 없으면 실패
                }
            }
            _planetTiles = tiles;
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
            }
            tile = null;
            return false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                var playerLookDir = AgentManager.Instance.Player.VisualTrm.transform.forward;
                _planetTiles = Get2By2TilesInAnyDirection(_planetTile, playerLookDir);
            }
        }


        public TileInteraction GetInteraction() => CurrentTile?.TileInteraction;
        public InteractType GetInteractType() => GetInteraction().InteractType;
    }
}
