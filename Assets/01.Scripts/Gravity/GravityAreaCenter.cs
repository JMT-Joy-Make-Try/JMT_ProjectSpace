using UnityEngine;

namespace JMT.Gravity
{
    public class GravityAreaCenter : GravityArea
    {
        public override Vector3 GetGravityDirection(GravityBody body)
        {
            return (transform.position - body.transform.position).normalized;
        }
    }
}
