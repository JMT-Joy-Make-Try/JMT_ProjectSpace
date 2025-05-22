using JMT.Item;
using JMT.UISystem.Interact;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileInteraction : MonoBehaviour
    {
        //[field: SerializeField] protected InteractionTileType interactionTileType;
        [field: SerializeField] protected ItemSO itemType;
        [field: SerializeField] public InteractType InteractType { get; private set; }
        [SerializeField] protected int itemCount;

        protected PlanetTile planetTile;

        protected virtual void Awake()
        {
            planetTile = transform.parent.GetComponent<PlanetTile>();
            //planetTile.OnClick += Interaction;
        }

        private void OnDestroy()
        {
            //planetTile.OnClick -= Interaction;
        }

        public virtual void Interaction()
        {
            planetTile.EdgeEnable(true);
        }

        public void SetType(InteractType interactType)
        {
            InteractType = interactType;
        }

        public void AddObject(GameObject obj)
        {
            Instantiate(obj, transform);
        }

        public void RemoveObject() => Destroy(gameObject);
    }
}
