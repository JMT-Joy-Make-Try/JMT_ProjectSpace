using UnityEngine;

namespace JMT.Planets.Tile
{
    public class MeshMerger : MonoBehaviour
    {
        public GameObject[] objectsToMerge;

        void Start()
        {
            CombineMeshes();
        }

        void CombineMeshes()
        {
            // 객체 배열에서 모든 메시를 합침
            CombineInstance[] combine = new CombineInstance[objectsToMerge.Length];
            MeshFilter[] meshFilters = new MeshFilter[objectsToMerge.Length];
        
            for (int i = 0; i < objectsToMerge.Length; i++)
            {
                meshFilters[i] = objectsToMerge[i].GetComponent<MeshFilter>();
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combine, true, true);

            // 새로 합친 메시를 이 오브젝트에 적용
            GetComponent<MeshFilter>().mesh = combinedMesh;
            GetComponent<MeshRenderer>().material = objectsToMerge[0].GetComponent<MeshRenderer>().material;
        }
    }
}