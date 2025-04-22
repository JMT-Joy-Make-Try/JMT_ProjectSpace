using System.Collections.Generic;
using JMT.Building;
using JMT.Planets.Tile;
using System;
using Unity.AI.Navigation;
using UnityEngine;
using Event = JMT.Planets.Events.Event;
using Random = UnityEngine.Random;
using JMT.UISystem;

namespace JMT.Planets
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private List<Event> events = new List<Event>();
        [SerializeField] private List<TileList> tileLists = new List<TileList>();
        [SerializeField] private NavMeshSurface navMeshSurface;
        
        public event Action EventWarning;

        private void Start()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDayCountEvent += HandleChangeDay;
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeDayCountEvent -= HandleChangeDay;
        }

        private void HandleChangeDay(int day)
        {
            if (day % 3 == 2)
            {
                EventWarning?.Invoke();
            }
            if (day % 3 == 0)
            {
                StartEvent();
            }
        }

        protected virtual void GeneratePlanet(TilesSO tilesSO)
        {
            for (int i = 0; i < tileLists.Count; i++)
            {
                if (i < tilesSO.tiles[i].Count)
                {
                    tileLists[i].SetTile(tilesSO.tiles[i].TileType, tilesSO.tiles[i].Color);
                }
                else
                {
                    tileLists[i].SetTile(TileType.Dead, Color.black);
                }
            }
            
            BakeNavMesh();
        }

        protected virtual void StartEvent()
        {
            foreach (var e in events)
            {
                if (Random.Range(0f, 1f) < e.Probability)
                {
                    e.StartEvent();
                    break;
                }
            }
        }
        
        protected virtual void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
            
        }
    }
}
