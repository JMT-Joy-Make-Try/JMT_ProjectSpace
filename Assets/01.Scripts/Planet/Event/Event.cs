using UnityEngine;

namespace JMT.Planets.Events
{
    public abstract class Event : MonoBehaviour
    {
        [field: SerializeField] public float Probability { get; protected set; }
        public abstract void StartEvent();

        public abstract void EndEvent();
    }
}
