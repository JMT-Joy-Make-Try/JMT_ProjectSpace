using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileInteraction : MonoBehaviour
    {
        [field: SerializeField] protected InteractionTileType interactionTileType;
        [field: SerializeField] protected ItemType itemType;
        [SerializeField] protected int itemCount;

        protected PlanetTile planetTile;

        private void Awake()
        {
            planetTile = transform.parent.GetComponent<PlanetTile>();
            Debug.Log("Test Click / TileInteraction.cs");
            planetTile.OnClick += Interaction;
        }

        private void OnDestroy()
        {
            planetTile.OnClick -= Interaction;
        }

        public virtual void Interaction(PlanetTile tile)
        {
            switch (interactionTileType)
            {
                case InteractionTileType.NoneTile:
                    Debug.Log("けいしぉ");
                    MainUI.Instance.NoneUI.OpenUI();
                    break;
                case InteractionTileType.ItemTile:
                    Debug.Log("けいしぉ");
                    MainUI.Instance.ItemUI.OpenUI();
                    break;
                case InteractionTileType.BuildingTile:
                    Debug.Log("けいしぉ");
                    MainUI.Instance.BuildingUI.OpenUI();
                    break;
            }
            //RemoveObject();
        }

        public void AddObject(GameObject obj)
        {
            Instantiate(obj, transform);
        }

        public void RemoveObject() => Destroy(gameObject);
    }
}
