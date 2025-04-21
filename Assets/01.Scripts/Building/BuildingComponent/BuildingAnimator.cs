using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingAnimator : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private Animator buildingAnimator;
        [SerializeField] private ParticleSystem buildingParticle;
        
        public BuildingBase Building { get; private set; }
        
        public void SetAnimation(bool isWalking)
        {
            if (buildingAnimator == null)
            {
                Debug.LogWarning("No animator attached to building");
                return;
            }

            if (buildingParticle != null)
            {
                if (isWalking)
                {
                    buildingParticle.Play();
                }
                else
                {
                    buildingParticle.Stop();
                }
            }

            buildingAnimator.SetBool("IsWalking", isWalking);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}