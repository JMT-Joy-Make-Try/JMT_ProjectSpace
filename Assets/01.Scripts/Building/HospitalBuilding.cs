using JMT.Building.Component;
using System.Collections;
using JMT.Core.Manager;
using JMT.Agent.State;
using UnityEngine;
using JMT.Agent;
using Random = UnityEngine.Random;

namespace JMT.Building
{
    public class HospitalBuilding : BuildingBase
    {
        [field: SerializeField] public float HealingTime = 5f;


        private void Start()
        {
            BuildingManager.Instance.HospitalBuildings.Add(this);
        }

        private void Heal(HospitalNPCData data)
        {
            StartCoroutine(HealingPatientRoutine(data));
        }

        private IEnumerator HealingPatientRoutine(HospitalNPCData data)
        {
            var patient = data.patient;
            yield return new WaitForSeconds(data.healingTime);
            patient.Init();
            var lodgingBuildings = BuildingManager.Instance.LodgingBuildings;
            var lodgingBuilding = lodgingBuildings[Random.Range(0, lodgingBuildings.Count)];
            patient.MovementCompo.Move(lodgingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position, patient.Health.MoveSpeed);
            patient.ClothCompo.ChangeCloth(AgentType.Base);
            patient.StateMachineCompo.ChangeState(NPCState.Move);
        }
    }
}