using JMT.Planets.Tile.Items;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileInteraction : MonoBehaviour
    {
        //[field: SerializeField] protected InteractionTileType interactionTileType;
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
            TileManager.Instance._currentTile = tile;
        }

        public void AddObject(GameObject obj)
        {
            Instantiate(obj, transform);
        }

        public void RemoveObject() => Destroy(gameObject);
    }
}
