using System;
using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentAI<T> : MonoBehaviour, IDamageable, IPoolable where T : Enum
    {
        [field:SerializeField] public StateMachine<T> StateMachineCompo { get; private set; }
        [field:SerializeField] public Animator AnimatorCompo { get; protected set; }
        [field:SerializeField] public AgentMovement MovementCompo { get; private set; }
        [field:SerializeField] public AgentCloth ClothCompo { get; private set; }
        [field:SerializeField] public AnimationEndTrigger AnimationEndTrigger { get; private set; }
        
        public bool IsDead { get; private set; }
        
        protected int _curHealth;
        
        protected event Action OnDeath;
       
        protected virtual void Awake()
        {
            StateMachineCompo = gameObject.GetComponentOrAdd<StateMachine<T>>();
            AnimatorCompo = gameObject.GetComponentOrAdd<Animator>();
            MovementCompo = gameObject.GetComponentOrAdd<AgentMovement>();
            ClothCompo = GetComponent<AgentCloth>();
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

        public void TakeDamage(int damage, bool isHeal)
        {
            _curHealth += isHeal ? damage : -damage;
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            if (IsDead) return;
            OnDeath?.Invoke();
            IsDead = true;
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
