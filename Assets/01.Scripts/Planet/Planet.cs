using System.Collections.Generic;
using JMT.Building;
using UnityEngine;
using Event = JMT.Planets.Events.Event;

namespace JMT.Planets
{
    public abstract class Planet : MonoBehaviour
    {
        [SerializeField] private List<BuildingBase> buildings = new List<BuildingBase>();
        [SerializeField] private List<Event> events = new List<Event>();

        protected virtual void GeneratePlanet()
        {
            
        }
        protected abstract void Rotate();

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
        
    }
}
