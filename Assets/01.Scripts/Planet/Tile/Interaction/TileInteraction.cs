using JMT.Item;
using JMT.UISystem.Interact;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class TileInteraction : MonoBehaviour
    {
        [field: SerializeField] protected ItemSO itemType;
        [field: SerializeField] public InteractType InteractType { get; private set; }
        [SerializeField] protected int itemCount;

        protected PlanetTile planetTile;

        protected virtual void Awake()
        {
            planetTile = transform.parent.GetComponent<PlanetTile>();
        }

        public virtual void Interaction()
        {
            planetTile.EdgeEnable(true);
        }

        public void SetType(InteractType interactType)
        {
            InteractType = interactType;
        }

        public GameObject AddObject(GameObject obj)
        {
            return Instantiate(obj, transform);
        }

        public void RemoveObject() => Destroy(gameObject);
    }
}
