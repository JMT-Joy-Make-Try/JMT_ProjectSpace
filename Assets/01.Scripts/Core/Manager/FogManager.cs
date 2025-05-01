using JMT.Core.Tool;
using JMT.Planets.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class FogManager : MonoSingleton<FogManager>
    {
        [SerializeField] private bool _isFogActive = true;
        [Space]
        [SerializeField] private TileList _defaultTileList;
        [SerializeField] private TileList _baseBuildingTileList;
        [SerializeField] private List<TileList> _fogObjects1Tier;
        [SerializeField] private List<TileList> _fogObjects2Tier;
        [SerializeField] private List<TileList> _fogObjects3Tier;

        private List<Fog> _allFogs = new(); // 모든 Fog 객체 캐싱

        private void Start()
        {
            ResetFog(_isFogActive);
            OffDefaultFog();
            CollectFogs(); // 모든 Fog 참조 수집
        }

        private void ResetFog(bool active)
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(active));
            _fogObjects1Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
            _fogObjects2Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
            _fogObjects3Tier.ForEach(t => t.Tiles.ForEach(tile => tile.Fog.SetFog(active)));
        }

        public void OffFogBaseBuilding()
        {
            _baseBuildingTileList.Tiles.ForEach(t => t.Fog.SetFog(false));
        }

        public void OffDefaultFog()
        {
            _defaultTileList.Tiles.ForEach(t => t.Fog.SetFog(false));
        }

        public bool IsAllFogOff(FogTier tier)
        {
            if (tier == FogTier.None) return true;

            var targetList = tier switch
            {
                FogTier.One => _fogObjects1Tier,
                FogTier.Two => _fogObjects2Tier,
                FogTier.Three => _fogObjects3Tier,
                _ => throw new ArgumentOutOfRangeException(nameof(tier), tier, null)
            };

            foreach (var tileList in targetList)
            {
                foreach (var tile in tileList.Tiles)
                {
                    if (tile.Fog.IsFogActive)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 모든 Fog들을 리스트로 수집 (중복 없이)
        /// </summary>
        private void CollectFogs()
        {
            _allFogs.Clear();

            _allFogs.AddRange(_defaultTileList.Tiles.ConvertAll(t => t.Fog));
            _allFogs.AddRange(_baseBuildingTileList.Tiles.ConvertAll(t => t.Fog));

            _fogObjects1Tier.ForEach(list => _allFogs.AddRange(list.Tiles.ConvertAll(t => t.Fog)));
            _fogObjects2Tier.ForEach(list => _allFogs.AddRange(list.Tiles.ConvertAll(t => t.Fog)));
            _fogObjects3Tier.ForEach(list => _allFogs.AddRange(list.Tiles.ConvertAll(t => t.Fog)));
        }

        /// <summary>
        /// 현재 위치 기준으로 가장 가까운 활성 안개 위치 반환
        /// </summary>
        public Vector3 GetNearestActiveFogPosition(Vector3 from)
        {
            Fog closest = null;
            float bestDistSqr = float.MaxValue;

            Vector2 fromXZ = new Vector2(from.x, from.z);

            foreach (var fog in _allFogs)
            {
                if (!fog.IsFogActive) continue;

                Vector3 fogPos = fog.transform.position;
                Vector2 fogXZ = new Vector2(fogPos.x, fogPos.z);
                float distSqr = (fogXZ - fromXZ).sqrMagnitude;

                if (distSqr < bestDistSqr)
                {
                    bestDistSqr = distSqr;
                    closest = fog;
                }
            }

            return closest != null ? closest.transform.position : from.GetRandomNearestPosition(20f);
        }


        public List<Fog> GetActiveFogs()
        {
            return _allFogs.FindAll(fog => fog.IsFogActive);
        }
    }
}
