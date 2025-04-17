using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentCloth<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private SerializedDictionary<T, Animator> agentClothList;

        public Animator CurrentCloth { get; private set; }

        public virtual void Init(T type)
        {
            CurrentCloth = agentClothList[type];
        }
        
        public virtual void SetCloth(T type)
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
        Patient,
        OrganicCollector
    }
}