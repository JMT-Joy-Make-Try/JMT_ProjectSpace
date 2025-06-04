using JMT.Agent.NPC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Hospital
{
    public class HospitalPatientStatView : SidePanelUI
    {
        [Tooltip("현재 치료중이라고 표시 뜨는 이미지")]
        [SerializeField] private Image therapyImage;
        //[SerializeField] private Image healthImage;
        [SerializeField] private List<CellUI> stats = new();

        public void SetStatPanel(NPCAgent npc)
        {
            // 여기어카냥 ㅎㅎ
        }
    }
}
