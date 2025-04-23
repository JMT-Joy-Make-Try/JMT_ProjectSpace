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
        }

        public void SetFloatProperty(string propertyName, float value)
        {
            visualMat.SetFloat(propertyName, value);
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
        
        public void BuildingTransparent(float value)
        {
            SetFloatProperty("_Alpha", value);
            SetShadowCastingMode(value < 1f ? ShadowCastingMode.Off : ShadowCastingMode.On);
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}