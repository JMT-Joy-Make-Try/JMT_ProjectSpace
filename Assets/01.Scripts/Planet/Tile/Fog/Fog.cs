using System.Collections.Generic;
using UnityEngine;
using JMT.Agent;
using JMT.Agent.NPC;

namespace JMT.Planets.Tile
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private float _playerInFogDamageTime = 5f;
        [SerializeField] private int _damageAmount = 10;
        [SerializeField] private GameObject _fogLightObject;
        private bool _isPlayerInFog = false;
        private float _curPlayerInFogTime = 0f;

        private List<NPCAgent> _npcAgents = new List<NPCAgent>();
        private Player.Player _player;
        public bool IsFogActive { get; private set; } 

        public void SetFog(bool active, bool lightActive = false)
        {
            gameObject.SetActive(active);
            _fogLightObject.SetActive(lightActive);
            IsFogActive = active;
        }

        private void Update()
        {
            if (_isPlayerInFog)
            {
                _curPlayerInFogTime += Time.deltaTime;
                if (_curPlayerInFogTime >= _playerInFogDamageTime)
                {
                    DamageNPC();
                    _curPlayerInFogTime = 0f;
                }
            }
        }

        private void DamageNPC()
        {
            foreach (var npcAgent in _npcAgents)
            {
                npcAgent.TakeDamage(_damageAmount, false);
                Debug.Log("Damaging NPC in fog");;
            }
            if (_player != null)
            {
                _player.TakeDamage(_damageAmount, false);
                Debug.Log("Damaging Player in fog");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NPCAgent npcAgent))
            {
                // todo: 경고 띄우기 - "message: 경고! 앞을 밝힐 무언가가 필요합니다!", 테두리 어둡게
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = true;
                _npcAgents.Add(npcAgent);
            }
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = player;
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out NPCAgent npcAgent))
            {
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = false;
                _npcAgents.Remove(npcAgent);
            }
            if (other.TryGetComponent(out Player.Player player))
            {
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = false;
                _player = null;
            }
        }
    }
}