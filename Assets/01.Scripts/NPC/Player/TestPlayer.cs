using UnityEngine;

namespace JMT
{
    public class TestPlayer : MonoBehaviour
    {
        void Update()
        {
            transform.position += Vector3.forward * Time.deltaTime;
        }
    }
}
