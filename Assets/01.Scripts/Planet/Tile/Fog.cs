using JMT.Object;
using System;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class Fog : TouchableObject
    {
        private void Awake()
        {
            OnClick += OnFogClickHandler;
        }
        
        private void OnDestroy()
        {
            OnClick -= OnFogClickHandler;
        }

        private void OnFogClickHandler()
        {
            
        }

        public void SetFog(bool active)
        {
            gameObject.SetActive(active);
        }
        
        
    }
}