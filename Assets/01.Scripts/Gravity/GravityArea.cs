using UnityEngine;

namespace JMT.Gravity
{
    public abstract class GravityArea : MonoBehaviour
    {
        [SerializeField] private int priority;
        public int Priority => priority;

        private void Start()
        {
            transform.GetComponent<Collider>().isTrigger = true;
        }

        public abstract Vector3 GetGravityDirection(GravityBody obj);

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out GravityBody body))
            {
                body.AddGravityArea(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out GravityBody body))
            {
                body.RemoveGravityArea(this);
            }
        }
    }
}
