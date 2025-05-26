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
using UnityEngine.VFX;

namespace JMT.Planets.Tile
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private GameObject _fogLightObject;
        [field: SerializeField] public int DamageAmount { get; private set; } = 1;
        
        private VisualEffect _fogParticleSystem;
        private BoxCollider _fogCollider;

        private float DefaultFogCount;

        public bool IsFogActive { get; private set; }

        private void Awake()
        {
            _fogParticleSystem = GetComponent<VisualEffect>();
            _fogCollider = GetComponent<BoxCollider>();
            DefaultFogCount = _fogParticleSystem.GetFloat("FogCount");
        }

        public void SetFog(bool active, bool lightActive = false)
        {
            if (active)
            {
                _fogParticleSystem.SetFloat("FogCount", DefaultFogCount);
                _fogParticleSystem.playRate = 1f;
            }
            else
            {
                _fogParticleSystem.SetFloat("FogCount", 0);
                _fogParticleSystem.playRate = 10f;
            }
            _fogLightObject.SetActive(lightActive);
            IsFogActive = active;
            _fogCollider.enabled = active;
        }
    }
}