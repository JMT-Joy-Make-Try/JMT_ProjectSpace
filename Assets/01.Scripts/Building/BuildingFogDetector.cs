using JMT.Agent;
using JMT.Planets.Tile;
using System;
using UnityEngine;

namespace JMT.Building
{
    public class BuildingFogDetector : MonoBehaviour
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private Renderer _renderer;
        private Collider[] _collider;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _collider = new Collider[2];
        }

        private void Update()
        {
            _renderer.material.SetColor(BaseColor, FogDetect() ? Color.black : Color.white);
        }
        
        private bool FogDetect()
        {
            int cnt = Physics.OverlapBoxNonAlloc(
                transform.position, 
                new Vector3(0.5f, 0.5f, 0.5f), 
                _collider, 
                Quaternion.identity);

            for (int i = 0; i < cnt; i++)
            {
                if (_collider[i].gameObject.TryGetComponent<Fog>(out var fog))
                {
                    if (fog.IsFogActive)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
