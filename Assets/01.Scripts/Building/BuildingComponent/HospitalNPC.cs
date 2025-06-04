using System.Collections.Generic;
using JMT.Agent.NPC;

namespace JMT.Building.Component
{
    public class HospitalNPC : BuildingNPC
    {
        private List<HospitalNPCData> _hospitalNPCData;
        public override void Init(BuildingBase building)
        {
            base.Init(building);
            _hospitalNPCData = new List<HospitalNPCData>();
        }
        
        public override void AddNpc(NPCAgent agent)
        {
            base.AddNpc(agent);
            _hospitalNPCData.Add(new(agent, null));
        }
        
        public void AddPatient(NPCAgent patient)
        {
            if (_hospitalNPCData.Count == 0) return;
            int index = _hospitalNPCData.FindIndex(data => data.patient == null);
            if (index != -1)
            {
                _hospitalNPCData[index].patient = patient;
                patient.WorkCompo.SetBuilding(Building);
            }
        }
    }

    [System.Serializable]
    public class HospitalNPCData
    {
        public float healingTime = 5f; // Time to heal the patient
        public NPCAgent doctor;
        public NPCAgent patient;
        
        public HospitalNPCData(NPCAgent doctor, NPCAgent patient)
        {
            this.doctor = doctor;
            this.patient = patient;
        }
    }
}