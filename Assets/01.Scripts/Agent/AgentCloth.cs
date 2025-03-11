using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentCloth : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<AgentType, List<GameObject>> agentClothList;

        private List<GameObject> _currentClothList;

        private void Awake()
        {
            _currentClothList = new List<GameObject>();
        }

        public void SetCloth(AgentType type)
        {
            if (_currentClothList.Count > 0)
            {
                foreach (var cloth in _currentClothList)
                {
                    cloth.SetActive(false);
                }
                _currentClothList.Clear();
            }
            foreach (var cloth in agentClothList[type])
            {
                cloth.SetActive(true);
                _currentClothList.Add(cloth);
            }
            
        }
    }

    public enum AgentType
    {
        Base,
        Patient,
        Doctor,
        Researcher,
        Pilot,
        Worker,
        Farmer,
        Cook,
        DustCollector,
    }
}