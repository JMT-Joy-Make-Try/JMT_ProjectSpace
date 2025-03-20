using System.Collections.Generic;
using UnityEngine;
using JMT.Agent;

namespace JMT.Planets.Tile
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private float _playerInFogDamageTime = 5f;
        [SerializeField] private GameObject _fogLightObject;
        private bool _isPlayerInFog = false;
        private float _curPlayerInFogTime = 0f;

        private List<NPCAgent> _npcAgents = new List<NPCAgent>();

        public void SetFog(bool active, bool lightActive = false)
        {
            gameObject.SetActive(active);
            _fogLightObject.SetActive(lightActive);
        }

        private void Update()
        {
            if (_isPlayerInFog)
            {
                _curPlayerInFogTime += Time.deltaTime;
                if (_curPlayerInFogTime >= _playerInFogDamageTime)
                {
                    DamageNPC();
                }
            }
        }

        private void DamageNPC()
        {
            foreach (var npcAgent in _npcAgents)
            {
                        
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<NPCAgent>(out NPCAgent npcAgent))
            {
                // todo: 경고 띄우기 - "message: 경고! 앞을 밝힐 무언가가 필요합니다!", 테두리 어둡게
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = true;
                _npcAgents.Add(npcAgent);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<NPCAgent>(out NPCAgent npcAgent))
            {
                _curPlayerInFogTime = 0f;
                _isPlayerInFog = false;
                _npcAgents.Remove(npcAgent);
            }
        }
    }
}