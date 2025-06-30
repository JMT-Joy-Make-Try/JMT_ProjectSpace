using JMT.Building;
using JMT.Item;
using JMT.Planets.Tile.Items;
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

        public ItemType GetItemType()
        {
            return itemType != null ? itemType.ItemType : ItemType.None;
        }

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

        public void RemoveObject() => Destroy(gameObject);

        public T AddObject<T>(T obj, Transform parent) where T : UnityEngine.Object
        {
            return Instantiate(obj, parent);
        }

        public T AddComponent<T>() where T : Component
        {
            return gameObject.AddComponent<T>();
        }

        public bool IsItemType(ItemType itemType)
        {
            return this.itemType != null && this.itemType.ItemType == itemType;
        }
        
        public T GetBuilding<T>() where T : BuildingBase
        {
            if (planetTile.CurrentBuilding is T building)
            {
                return building;
            }
            return null;
        }
    }
}
