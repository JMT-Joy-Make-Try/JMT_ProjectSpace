using UnityEngine;

namespace JMT.Planets.Field
{
    public class Field : MonoBehaviour
    {
        [field: SerializeField] public Transform[] PlantPositions { get; private set; }
    }
}