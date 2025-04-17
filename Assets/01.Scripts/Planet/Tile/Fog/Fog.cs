using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Core.Manager;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Rendering.Universal;

namespace JMT.Planets.Tile
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private GameObject _fogLightObject;
        [field: SerializeField] public int DamageAmount { get; private set; } = 1;
        
        private ParticleSystem _fogParticleSystem;
        private Renderer _fogRenderer;
        
        private Material _fogMaterial;
        
        private Vector3 playerPos;
        
        private AgentManager _agentManager;

        private void Awake()
        {
            _fogParticleSystem = GetComponent<ParticleSystem>();
            _fogRenderer = _fogParticleSystem.GetComponent<Renderer>();
            _fogMaterial = _fogRenderer.material;
            _agentManager = AgentManager.Instance;
        }

        private void Update()
        {
            // playerPos = _agentManager.PlayerTransform.position;
            // if (Vector3.Distance(transform.position, playerPos) < 30f)
            // {
            //     _fogParticleSystem.Play();
            //     Debug.Log("Fog Particle System Playing");
            // }
            // else
            // {
            //     _fogParticleSystem.Stop();
            //     Debug.Log("Fog Particle System Stopped");
            // }
        }

        public bool IsFogActive { get; private set; } 

        public void SetFog(bool active, bool lightActive = false)
        {
            // if (active)
            // {
            //     _fogParticleSystem.Play();
            // }
            // else
            // {
            //     _fogParticleSystem.Stop();
            //     gameObject.SetActive(false);
            // }
            gameObject.SetActive(active);
            _fogLightObject.SetActive(lightActive);
            IsFogActive = active;
        }

        // private void OnBecameInvisible()
        // {
        //     if (_fogParticleSystem.isPlaying)
        //     {
        //         _fogParticleSystem.Stop();
        //         Debug.Log("Fog Particle System Stopped");
        //     }
        // }
        //
        // private void OnBecameVisible()
        // {
        //     if (!_fogParticleSystem.isPlaying)
        //     {
        //         _fogParticleSystem.Play();
        //         Debug.Log("Fog Particle System Playing");
        //     }
        // }
    }
}