using DG.Tweening;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JMT.Agent
{
    public class FogDetect : MonoBehaviour
    {
        [SerializeField] private float _playerInFogDamageTime = 5f;
        [SerializeField] private float _playerInFogRadius = 1.5f;
        
        private bool _isPlayerInFog = false;
        private float _curPlayerInFogTime = 0f;
        
        private Collider[] _colliders;
        
        private IDamageable _agent;
        
        private event Action<bool> OnFogDetected;
        private int _damage = 1;

        private bool _prevIsPlayerInFog = false;

        private void Awake()
        {
            _colliders = new Collider[10];
            OnFogDetected += HandleFogDetected;
        }


        private void OnDestroy()
        {
            OnFogDetected -= HandleFogDetected;
        }

        private void HandleFogDetected(bool isInFog)
        {
            Player.Player player = _agent as Player.Player;
            FogSpeed(player, isInFog);
            FogCamera(player, isInFog);
        }

        public void Init(IDamageable agent)
        {
            _agent = agent;
        }

        private void Update()
        {
            DetectFog();
            FogDamage();
            
            if (_prevIsPlayerInFog != _isPlayerInFog)
            {
                OnFogDetected?.Invoke(_isPlayerInFog);
                _prevIsPlayerInFog = _isPlayerInFog;
            }
        }

        private void FogCamera(Player.Player player, bool isInFog)
        {
            if (player == null) return;
            var vignettes = VolumeManager.Instance.GetAllVolume<Vignette>();
            if (isInFog)
            {
                DOVirtual.Float(0.2f, 0.5f, 1f, (x) =>
                {
                    for (int i = 0; i < vignettes.Count; i++)
                    {
                        vignettes[i].intensity.value = x;
                    }
                });

                Camera.main.DOZoom(8f, 0.7f, Ease.OutQuad);
            }
            else
            {
                DOVirtual.Float(0.5f, 0.2f, 1f, (x) =>
                {
                    for (int i = 0; i < vignettes.Count; i++)
                    {
                        vignettes[i].intensity.value = x;
                    }
                });
                Camera.main.DOZoom(12f, 0.5f, Ease.InQuad);
            }
        }

        private void FogSpeed(Player.Player player, bool isInFog)
        {
            if (isInFog)
            {
                player.Movement.SetMoveSpeedMultiplier(0.5f);
                player.SetMultiplier(2);
            }
            else
            {
                player.Movement.ResetMoveSpeed();
                player.SetMultiplier(1);
            }
        }

        private void DetectFog()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, _playerInFogRadius, _colliders);
            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out Fog fog))
                {
                    _isPlayerInFog = true;
                    _damage = fog.DamageAmount;
                    return;
                }
            }

            _isPlayerInFog = false;
        }

        private void FogDamage()
        {
            if (_isPlayerInFog)
            {
                _curPlayerInFogTime += Time.deltaTime;
                if (_curPlayerInFogTime >= _playerInFogDamageTime)
                {
                    _agent.TakeDamage(_damage);
                    Debug.Log(_damage);
                    _curPlayerInFogTime = 0f;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _playerInFogRadius);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2f);
        }
    }
}