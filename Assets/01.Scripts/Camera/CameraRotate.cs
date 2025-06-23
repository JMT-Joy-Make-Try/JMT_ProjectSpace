using UnityEngine;

namespace JMT
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 0.3f;

        private void Update()
        {
            Camera.main.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
