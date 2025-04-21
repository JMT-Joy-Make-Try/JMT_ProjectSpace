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
        

        public bool IsFogActive { get; private set; } 

        public void SetFog(bool active, bool lightActive = false)
        {
            gameObject.SetActive(active);
            _fogLightObject.SetActive(lightActive);
            IsFogActive = active;
        }
    }
}