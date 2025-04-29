using DG.Tweening;
using JMT.Core;
using JMT.UISystem.Interact;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class Alien : AgentAI<AlienState>, IKnockbackable, IStunable
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public AlienTargetFinder TargetFinder { get; private set; }
        [field: SerializeField] public Attacker Attacker { get; private set; }
        
        [field: SerializeField] public List<StateColor> StateColor { get; private set; }

        public Rigidbody RbCompo => GetComponent<Rigidbody>();

        [SerializeField] public SkinnedMeshRenderer AlienRenderer;
        
        protected override void Awake()
        {
            Debug.Log(AlienRenderer.material);
            AlienRenderer.material = Instantiate(AlienRenderer.material);
            base.Awake();
            OnDeath += HandleDeath;
            StateMachineCompo.ChangeState(AlienState.Idle);
        }

        protected void OnDestroy()
        {
            OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            StateMachineCompo.ChangeState(AlienState.Dead, true);
        }

        public override void Init()
        {
            base.Init();
            StateMachineCompo.ChangeState(AlienState.Idle);
            MovementCompo.Stop(false);
            AlienRenderer.material.SetFloat("_DissolveValue", -1);
        }
        
        public void ChangeColor(AlienState state)
        {
            var color = StateColor.Find(x => x.state == state).color;
            AlienRenderer.material.DOColor(color, "_Color", 0.2f);
        }

        public void Knockback(Vector3 direction, float force)
        {
            RbCompo.AddForce(direction * force, ForceMode.Impulse);
        }

        public void Stun(float stunTime)
        {
            StartCoroutine(StunCoroutine(stunTime));
        }

        private IEnumerator StunCoroutine(float stunTime)
        {
            MovementCompo.Stop(true);
            yield return new WaitForSeconds(stunTime);
            MovementCompo.Stop(false);
        }
    }

    [Serializable]
    public struct StateColor
    {
        public AlienState state;
        [ColorUsage(true, true)]
        public Color color;
    }
}