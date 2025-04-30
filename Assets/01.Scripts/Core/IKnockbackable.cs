using UnityEngine;

namespace JMT.Core
{
    /// <summary>
    /// Interface for objects that can be knocked back.
    /// </summary>
    public interface IKnockbackable
    {
        /// <summary>
        /// Gets the Rigidbody component of the object.
        /// </summary>
        Rigidbody RbCompo { get; }

        /// <summary>
        /// Applies a knockback force to the object in the specified direction.
        /// </summary>
        /// <param name="direction">The direction of the knockback.</param>
        /// <param name="force">The magnitude of the knockback force.</param>
        void Knockback(Vector3 direction, float force);
    }
}