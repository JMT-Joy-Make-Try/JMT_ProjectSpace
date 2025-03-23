using JMT.Agent.Alien;
using JMT.Core;
using JMT.Core.Tool;
using UnityEngine;

namespace JMT.Agent.State
{
    public class AlienMoveState : State<AlienState>
    {
        private int _count = 0;
        private Collider[] _colliders = new Collider[10];
        private Alien.Alien _alien;

        public override void Initialize(AgentAI<AlienState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _alien = (Alien.Alien)agent;
        }

        public override void EnterState()
        {
            base.EnterState();
            _agent.MovementCompo.Move(Vector3.zero, _alien.MoveSpeed);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            DetectObject();
        }

        private void DetectObject()
        {
            _count = Physics.OverlapSphereNonAlloc(_agent.transform.position, _alien.AttackRange, _colliders, _alien.WhatIsAttackable);
            FindTarget();
        }

        private void FindTarget()
        {
            if (_count > 0)
            {
                // todo: 타겟을 찾는 로직을 수정해야함
                for (int i = 0; i < _count; i++)
                {
                    if (_colliders[i].TryGetComponent(out Player.Player player))
                    {
                        _alien.Target = player.transform;
                        break;
                    }

                    if (_colliders[i].TryGetComponent(out Building.BuildingBase building))
                    {
                        _alien.Target = building.transform;
                        break;
                    }

                    if (_colliders[i].TryGetComponent(out NPC.NPCAgent npc))
                    {
                        _alien.Target = npc.transform;
                        break;
                    }
                }
                _agent.MovementCompo.Stop(true);
                _agent.StateMachineCompo.ChangeState(AlienState.Attack1);
                Debug.Log("Target Found");
            }
        }
    }
}