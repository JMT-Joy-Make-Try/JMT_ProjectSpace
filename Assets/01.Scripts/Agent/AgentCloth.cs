using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentCloth : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<AgentType, Animator> agentClothList;

        public Animator CurrentCloth { get; private set; }

        private void Start()
        {
            SetCloth(AgentType.Base);
        }


        public void SetCloth(AgentType type)
        {
            if (CurrentCloth != null)
            {
                CurrentCloth.gameObject.SetActive(false);
            }

            CurrentCloth = agentClothList[type];
            CurrentCloth.gameObject.SetActive(true);
        }
    }

    public enum AgentType
    {
        Base,
        DustCollector,
        Guard,
        FuelCollector,
        Patient
    }
}