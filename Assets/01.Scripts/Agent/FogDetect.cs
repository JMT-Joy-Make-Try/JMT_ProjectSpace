using DG.Tweening;
using JMT.CameraSystem;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.PlayerCharacter;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JMT.Agent
{
    public class FogDetect : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        private event Action<bool> OnFogDetected;
        
        [SerializeField] private float _playerInFogDamageTime = 5f;
        [SerializeField] private float _playerInFogRadius = 1.5f;
        
        private bool _isPlayerInFog = false;
        public bool IsPlayerInFog => _isPlayerInFog;
        private bool _prevIsPlayerInFog = false;
        
        private int _damage = 1;
        private float _curPlayerInFogTime = 0f;
        
        
        private Collider[] _colliders;
        
        public void Init(IPlayer player)
        {
            Player = player as Player;
            _colliders = new Collider[10];
            OnFogDetected += HandleFogDetected;
        }
        
        private void OnDestroy()
        {
            OnFogDetected -= HandleFogDetected;
        }

        private void HandleFogDetected(bool isInFog)
        {
            FogSpeed(Player, isInFog);
            FogCamera(Player, isInFog);
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

        private void FogCamera(Player player, bool isInFog)
        {
            if (player == null) return;
            var vignettes = VolumeManager.Instance.GetAllVolume<Vignette>();
            if (isInFog)
            {
                CameraEffect(vignettes,0.2f, 0.5f, 1f);
                CameraManager.Instance.MainCamera.DOZoom(8f, 0.7f, Ease.OutQuad);
            }
            else
            {
                CameraEffect(vignettes, 0.5f, 0.2f, 1f);
                CameraManager.Instance.MainCamera.DOZoom(12f, 0.5f, Ease.InQuad);
            }
        }

        

        private void FogSpeed(Player player, bool isInFog)
        {
            if (isInFog)
            {
                player.MovementCompo.SetMoveSpeedMultiplier(0.5f);
                player.HealthCompo.SetOxygenMultiplier(3);
            }
            else
            {
                player.MovementCompo.ResetMoveSpeed();
                player.HealthCompo.SetOxygenMultiplier(1);
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
                    Player.HealthCompo.TakeDamage(_damage);
                    _curPlayerInFogTime = 0f;
                }
            }
        }

        private void CameraEffect(List<Vignette> vignettes, float from, float to, float duration)
        {
            DOVirtual.Float(from, to, duration, x =>
            {
                foreach (var t in vignettes)
                {
                    t.intensity.value = x;
                }
            });
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _playerInFogRadius);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2f);
        }
    }
}