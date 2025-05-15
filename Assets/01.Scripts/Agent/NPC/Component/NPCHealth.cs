using JMT.Agent.NPC;
using JMT.Core.Tool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Range = JMT.Core.Tool.Range;

namespace JMT.Agent
{
    public class NPCHealth : AgentHealth, INPCComponent
    {
        public NPCAgent Agent { get; private set; }
        [field: SerializeField] public NPCData Data { get; set; }
        
        [Space]
        [SerializeField] private List<Range> _healthRange;
        [field:SerializeField] public int MoveSpeed;
        [field:SerializeField] public int WorkSpeed;
        
        public event Action<bool> OnHealthWarningEvent;
        
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }
        
        public override void TakeDamage(int damage, bool isHeal)
        {
            base.TakeDamage(damage, isHeal);
            int healthPercent = Mathf.RoundToInt(_curHealth * 100 / Health);
            if (healthPercent < 40)
                OnHealthWarningEvent?.Invoke(true);
            else
                OnHealthWarningEvent?.Invoke(true);
        }
        
        protected void SetSpeed()
        {
            WorkSpeed = GetPercent(_curHealth);
            MoveSpeed = MathExtension.GetPercentageValue(MoveSpeed, GetPercent(_curHealth));

            if (WorkSpeed <= 0)
            {
                Agent.ClothCompo.ChangeCloth(AgentType.Patient);
            }
        }

        

        protected int GetPercent(float health)
        {
            int healthPercent = Mathf.RoundToInt(health * 100 / Health);
            int rangeValue = healthPercent.GetRangeValue(_healthRange);
            var findRange = _healthRange.Find(s => s.ReturnValue == rangeValue);
            var rangeIndex = _healthRange.IndexOf(findRange);

            if (rangeIndex == 2)
                OnHealthWarningEvent?.Invoke(true);
            else
                OnHealthWarningEvent?.Invoke(true);
            return rangeValue;
        }
        
    }
}