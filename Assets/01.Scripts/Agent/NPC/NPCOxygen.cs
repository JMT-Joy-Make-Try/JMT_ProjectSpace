using JMT.Core;
using UnityEngine;

namespace JMT.Agent.NPC
{
    public class NPCOxygen : MonoBehaviour, IOxygen
    {
        [field: SerializeField]public int Oxygen { get; private set; }
        public void AddOxygen(int value)
        {
            Oxygen += value;
        }
    }
}