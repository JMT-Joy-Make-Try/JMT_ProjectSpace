using JMT.Building;
using JMT.Building.Component;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class BuildingCombineManager : MonoSingleton<BuildingCombineManager>
    {
        private List<BuildingBase> _combineBuildingList = new List<BuildingBase>();
        private List<MeshFilter> _meshFilterList = new List<MeshFilter>();
        
        private MeshFilter _meshFilter;

        protected override void Awake()
        {
            base.Awake();
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void AddCombineBuilding(BuildingBase building)
        {
            if (!_combineBuildingList.Contains(building))
            {
                _combineBuildingList.Add(building);
                building.GetBuildingComponent<BuildingVisual>().MeshFilters.ForEach(x => _meshFilterList.Add(x));
            }
        }
        
        public void RemoveCombineBuilding(BuildingBase building)
        {
            if (_combineBuildingList.Contains(building))
            {
                _combineBuildingList.Remove(building);
                building.GetBuildingComponent<BuildingVisual>().MeshFilters.ForEach(x => _meshFilterList.Remove(x));
            }
        }
        
        public void ClearCombineBuilding()
        {
            _combineBuildingList.Clear();
            _meshFilterList.Clear();
        }

        public void CombineMesh()
        {
            if (_meshFilterList.Count == 0)
                return;

            CombineInstance[] combine = new CombineInstance[_meshFilterList.Count];
            for (int i = 0; i < _meshFilterList.Count; i++)
            {
                combine[i].mesh = _meshFilterList[i].sharedMesh;
                combine[i].transform = _meshFilterList[i].transform.localToWorldMatrix;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combine, true, true);
            _meshFilter.sharedMesh = combinedMesh;
        }
    }
}