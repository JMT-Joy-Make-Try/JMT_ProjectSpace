using JMT.Android.Vibration;
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
        [SerializeField] private int _eventPlayDay = 0;
        
        public event Action EventWarning;

        protected virtual void Awake()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDayCountEvent += HandleChangeDay;
            VibrationUtil.Init();
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeDayCountEvent -= HandleChangeDay;
        }

        private void HandleChangeDay(int day)
        {
            StartEvent();
            /*if (day % _eventPlayDay == 2)
            {
                EventWarning?.Invoke();
            }
            if (day % _eventPlayDay == 0)
            {
                StartEvent();
            }*/
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
            Event selected = null;

            foreach (var e in events)
            {
                if (Random.Range(0f, 100f) < e.Probability)
                {
                    selected = e;
                    break;
                }
            }

            if (selected == null)
            {
                selected = events[Random.Range(0, events.Count)];
            }

            selected?.StartEvent();
        }

        
        protected virtual void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
            
        }
    }
}
