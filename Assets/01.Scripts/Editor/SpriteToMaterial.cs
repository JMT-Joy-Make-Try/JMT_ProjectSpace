using System.IO;
using UnityEditor;
using UnityEngine;

namespace JMT.Editor
{
    public class SpriteToMaterial
    {
        [MenuItem("Assets/Create/Material From Sprite", false, 1)]
        public static void CreateMaterialFromSprite()
        {
            UnityEngine.Object selected = Selection.activeObject;

            if (selected is Texture2D texture)
            {
                // 기본 Shader (URP 기준)
                Shader shader = Shader.Find("Universal Render Pipeline/Lit");
                if (shader == null)
                {
                    Debug.LogError("URP Shader not found. Make sure you're using URP.");
                    return;
                }
                

                Material material = new Material(shader);
                material.SetTexture("_BaseMap", texture);

                // 저장 경로
                string path = AssetDatabase.GetAssetPath(texture);
                string directory = Path.GetDirectoryName(path);
                string fileName = Path.GetFileNameWithoutExtension(path);
                string materialPath = Path.Combine(directory, fileName + "_Material.mat");

                // 저장
                AssetDatabase.CreateAsset(material, materialPath);
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = material;
            }
            else
            {
                Debug.LogWarning("선택한 항목이 Texture2D가 아닙니다. Sprite 텍스처를 선택하세요.");
            }
        }

        [MenuItem("Assets/Create/Material From Sprite", true)]
        public static bool ValidateCreateMaterialFromSprite()
        {
            return Selection.activeObject is Texture2D;
        }
    }
}
