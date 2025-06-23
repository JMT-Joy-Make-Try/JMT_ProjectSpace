using JMT.Agent.NPC;
using JMT.Building;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JMT.UISystem.Hospital
{
    public class HospitalPatientView : PanelUI
    {
        public event Action<NPCAgent> OnClickPatientEvent;
        private Action[] handlers;

        [SerializeField] private Transform patientContent;
        [SerializeField] private TextMeshProUGUI patientCountText;
        [SerializeField] private NPCContentUI patientPrefab;

        public void SetPatient(List<NPCAgent> patients)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                int value = i;
                handlers[value] = () => OnClickPatientEvent?.Invoke(patients[value]);
                NPCContentUI content = Instantiate(patientPrefab, patientContent);
                content.SetWorkerPanel(patients[value]);
                content.OnAddEvent += handlers[value];
            }
        }

        public void SetPatientText(HospitalBuilding building)
        {
            patientCountText.text = "기능 구현 필요";
        }

        private void OnDestroy()
        {
            ResetWorkerContent();
        }

        public override void CloseUI()
        {
            base.CloseUI();
            ResetWorkerContent();
        }

        private void ResetWorkerContent()
        {
            for (int i = 0; i < patientContent.childCount; ++i)
            {
                int value = i;
                NPCContentUI content = patientContent.GetChild(value).GetComponent<NPCContentUI>();
                content.OnAddEvent -= handlers[value];
                Destroy(content.gameObject);
            }
        }
    }
}
