using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Hospital
{
    public class HospitalPatientView : PanelUI
    {
        private Action[] handler;
        [SerializeField] private Transform patientContent;

        private List<WorkerContentUI> patients = new();

        private void Awake()
        {
            for(int i = 0; i <  patients.Count; i++)
            {
                int value = i;
                handler[value] = () => AddPatient(value);
                patients[i].OnAddEvent += handler[i];
            }
        }

        private void AddPatient(int index)
        {
            // 로직
        }
    }
}
