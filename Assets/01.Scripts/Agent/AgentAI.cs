using System;
using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.UISystem;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentAI<T> : MonoBehaviour, IDamageable, IPoolable where T : Enum
    {
        [field:SerializeField] public StateMachine<T> StateMachineCompo { get; private set; }
        [field:SerializeField] public Animator AnimatorCompo { get; protected set; }
        [field:SerializeField] public AgentMovement MovementCompo { get; private set; }
        [field:SerializeField] public NPCCloth ClothCompo { get; private set; }
        [field:SerializeField] public AnimationEndTrigger AnimationEndTrigger { get; private set; }

        private FillBarUI hpFillBarUI;
        
        public bool IsDead { get; private set; }
        
        protected int _curHealth;
        
        protected event Action OnDeath;
       
        protected virtual void Awake()
        {
            hpFillBarUI = GetComponent<FillBarUI>();
            StateMachineCompo = gameObject.GetComponentOrAdd<StateMachine<T>>();
            AnimatorCompo = gameObject.GetComponentOrAdd<Animator>();
            MovementCompo = gameObject.GetComponentOrAdd<AgentMovement>();
            ClothCompo = GetComponent<NPCCloth>();
            AnimationEndTrigger = gameObject.GetComponentOrAdd<AnimationEndTrigger>();
            
            StateMachineCompo.InitAllState(this);
            Init();
        }

        protected virtual void Update()
        {
            StateMachineCompo.UpdateState();
        }


        [field: SerializeField] public int Health { get; set; }
        public void InitStat()
        {
            _curHealth = Health;
        }

        public virtual void TakeDamage(int damage, bool isHeal)
        {
            _curHealth += isHeal ? damage : -damage;
            hpFillBarUI?.SetHpBar(_curHealth, Health);
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath?.Invoke();
            Debug.Log(gameObject.name + " is dead");
        }

        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        public void ResetItem()
        {
            Init();
        }

        public virtual void Init()
        {
            InitStat();
            IsDead = false;
            MovementCompo.Stop(false);
        }
    }
}
