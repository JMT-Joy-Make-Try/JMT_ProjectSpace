using UnityEditor;
using UnityEngine;

namespace JMT.Planets.Tile.Editor
{
    [CustomEditor(typeof(FogData))]
    public class FogDataEditor : UnityEditor.Editor
    {
        private FogData _fogData;
        
        private void OnEnable()
        {
            _fogData = (FogData)target;
        }
        
        public override void OnInspectorGUI()
        {
            
        }
    }
}