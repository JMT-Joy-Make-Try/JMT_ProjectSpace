using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Gravity
{
    public class GravityBody : MonoBehaviour
    {
        private static float gravityForce = 800;
        private Rigidbody rigidCompo;
        [SerializeField] private List<GravityArea> gravityAreas;
        public Vector3 GravityDirection
        {
            get
            {
                if(gravityAreas.Count == 0) return Vector3.zero;
                gravityAreas.Sort((area1, area2) => area1.Priority.CompareTo(area2.Priority));
                return gravityAreas.Last().GetGravityDirection(this).normalized;
            }
        }

        private void Awake()
        {
            rigidCompo = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidCompo.AddForce(GravityDirection * (gravityForce * Time.fixedDeltaTime), ForceMode.Acceleration);

            Quaternion upRotation = Quaternion.FromToRotation(transform.up, GravityDirection);
            Quaternion newRotation = Quaternion.Slerp(rigidCompo.rotation, upRotation * rigidCompo.rotation, Time.fixedDeltaTime * 3f);
            rigidCompo.MoveRotation(newRotation);
        }

        public void AddGravityArea(GravityArea area)
        {
            gravityAreas.Add(area);
        }
        public void RemoveGravityArea(GravityArea area)
        {
            gravityAreas.Remove(area);
        }
    }
}
