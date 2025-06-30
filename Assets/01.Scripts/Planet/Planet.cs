using JMT.Android.Vibration;
using System.Collections.Generic;
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
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private int _eventPlayDay = 0;
        
        public event Action EventWarning;

        protected virtual void Awake()
        {
            VibrationUtil.Init();
        }

        
        private void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
