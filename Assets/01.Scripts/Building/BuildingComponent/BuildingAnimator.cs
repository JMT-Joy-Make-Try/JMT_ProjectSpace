using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingAnimator : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private Animator buildingAnimator;
        [SerializeField] private ParticleSystem buildingParticle;
        
        public BuildingBase Building { get; private set; }
        
        public void SetAnimation(bool isWorking)
        {
            if (buildingAnimator == null)
            {
                Debug.LogWarning("No animator attached to building");
                return;
            }

            if (buildingParticle != null)
            {
                if (isWorking)
                {
                    buildingParticle.Play();
                }
                else
                {
                    buildingParticle.Stop();
                }
            }

            buildingAnimator.SetBool("IsWorking", isWorking);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}