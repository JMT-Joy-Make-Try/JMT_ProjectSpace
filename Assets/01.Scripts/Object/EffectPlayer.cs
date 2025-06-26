using UnityEngine;

namespace JMT.Effect
{
    public class EffectPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void PlayEffect()
        {
            if (_particleSystem == null)
            {
                Debug.LogWarning("Particle system is not assigned.");
                return;
            }

            _particleSystem.Play();
        }
        
        public void StopEffect()
        {
            if (_particleSystem == null)
            {
                Debug.LogWarning("Particle system is not assigned.");
                return;
            }

            _particleSystem.Stop();
        }
    }
}