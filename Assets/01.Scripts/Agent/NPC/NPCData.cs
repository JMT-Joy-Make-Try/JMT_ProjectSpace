using UnityEngine;

namespace JMT.Agent
{
    [System.Serializable]
    public struct NPCData
    {
        public float MaxHealth;
        [HideInInspector] public float Health;
        public float MaxOxygen;
        [HideInInspector] public float OxygenAmount;
    }
}