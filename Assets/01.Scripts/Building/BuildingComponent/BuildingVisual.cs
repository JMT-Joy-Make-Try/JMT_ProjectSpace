using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JMT.Building.Component
{
    public class BuildingVisual : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private Material visualMat;
        [SerializeField] private List<MeshRenderer> rendererList;
        
        public BuildingBase Building { get; private set; }
        public Material VisualMat => visualMat;
        public List<MeshFilter> MeshFilters => rendererList.ConvertAll(renderer => renderer.GetComponent<MeshFilter>());

        private void Start()
        {
            visualMat = Instantiate(visualMat);
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material = Instantiate(rendererList[i].material);
                }
            }
        }

        public void SetFloatProperty(string propertyName, float value, bool isAllRendererChange = false)
        {
            visualMat.SetFloat(propertyName, value);
            if (!isAllRendererChange) return;
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material.SetFloat(propertyName, value);
                }
            }
        }
        
        public void SetShadowCastingMode(ShadowCastingMode mode)
        {
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].shadowCastingMode = mode;
                }
            }
        }

        public void SetMaterial(Material material)
        {
            for (byte i = 0; i < rendererList.Count; i++)
            {
                if (rendererList[i] != null)
                {
                    rendererList[i].material = material;
                }
            }
        }
        
        public void BuildingTransparent(float value, bool isAllRendererChange = false)
        {
            SetFloatProperty("_Alpha", value, isAllRendererChange);
            SetShadowCastingMode(value < 1f ? ShadowCastingMode.Off : ShadowCastingMode.On);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}