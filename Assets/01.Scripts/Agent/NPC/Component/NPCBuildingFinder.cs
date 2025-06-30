using JMT.Agent.NPC;
using JMT.Building;
using JMT.Core.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class NPCBuildingFinder : MonoBehaviour, INPCComponent
    {
        public NPCAgent Agent { get; private set; }

        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }

        public async Awaitable<T> FindNearbyBuilding<T>() where T : BuildingBase
        {
            List<T> buildings = null;
            if (typeof(T) == typeof(HospitalBuilding))
                buildings = BuildingManager.Instance.HospitalBuildings as List<T>;
            else if (typeof(T) == typeof(OxygenBuilding))
                buildings = BuildingManager.Instance.OxygenBuildings as List<T>;
            else if (typeof(T) == typeof(LodgingBuilding))
                buildings = BuildingManager.Instance.LodgingBuildings as List<T>;
        
            if (buildings == null || buildings.Count == 0)
                return null;
        
            Vector3 agentPos = Agent.transform.position;
            T nearest = null;
            float minDist = float.MaxValue;
        
            int count = buildings.Count;
            
            float[] distances = new float[count];

            for (int i = 0; i < count; i++)
            {
                distances[i] = Vector3.Distance(agentPos, buildings[i].transform.position);
            }
            if (count < 10)
            {
                for (int i = 0; i < count; i++)
                {
                    if (distances[i] < minDist)
                    {
                        minDist = distances[i];
                        nearest = buildings[i];
                    }
                }
                return nearest;
            }
        
            await Awaitable.BackgroundThreadAsync();
            for (int i = 0; i < count; i++)
            {
                if (distances[i] < minDist)
                {
                    minDist = distances[i];
                    nearest = buildings[i];
                }
            }
            await Awaitable.MainThreadAsync();
            return nearest;
        }
    }
}