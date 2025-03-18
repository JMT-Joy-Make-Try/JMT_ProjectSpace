using UnityEngine;

namespace JMT.Object
{
    public class ItemObject : MonoBehaviour, ISpawnable
    {
        public void Spawn(Vector3 position)
        {
            transform.position = position;
        }
    }
}