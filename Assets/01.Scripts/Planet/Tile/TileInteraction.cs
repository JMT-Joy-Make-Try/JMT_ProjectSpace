using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileInteraction : MonoBehaviour
    {
        [field: SerializeField] private InteractionTileType interactionTileType;

        private PlanetTile planetTile;

        private void Awake()
        {
            planetTile = transform.parent.GetComponent<PlanetTile>();
            planetTile.OnClick += Interaction;
        }

        public void Interaction(PlanetTile tile)
        {
            switch (interactionTileType)
            {
                case InteractionTileType.NoneTile:
                    Debug.Log("�Ǽ��Ͻ�?");
                    break;
                case InteractionTileType.ItemTile:
                    Debug.Log("ĳ��?");
                    RemoveObject();
                    break;
                case InteractionTileType.BuildingTile:
                    Debug.Log("�𸣰ڴ�");
                    RemoveObject();
                    break;
            }
        }

        public void AddObject(GameObject obj)
        {
            Instantiate(obj, transform);
        }

        private void RemoveObject()
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
