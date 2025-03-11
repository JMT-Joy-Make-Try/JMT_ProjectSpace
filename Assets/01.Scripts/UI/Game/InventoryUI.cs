using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem
{
    public class InventoryUI : PanelUI
    {
        [SerializeField] private Transform content;
        private List<ItemCellUI> cells = new();

        private void Awake()
        {
            cells = content.GetComponentsInChildren<ItemCellUI>().ToList();
            OpenUI();
        }
        public override void OpenUI()
        {
            SerializedDictionary<ItemType, int> dic = InventoryManager.Instance.GetDictionary();

            int index = 0;
            foreach (var pair in dic)
            {
                if (index < cells.Count)
                    cells[index].SetItemCell(GetName(pair.Key), pair.Value);
                index++;
            }

            base.OpenUI();
        }

        private string GetName(ItemType key)
        {
            switch (key)
            {
                case ItemType.LiquidFuel:
                    return "Liquid Fuel";
                case ItemType.OrganicMatter:
                    return "Organic Matter";
                case ItemType.SpaceDust:
                    return "Space Dust";
                case ItemType.FlameIron:
                    return "Flame Iron";
                case ItemType.IceIron:
                    return "Ice Iron";
                case ItemType.Techron:
                    return "Techron";
                case ItemType.DustBundle:
                    return "Dust Bundle";
                case ItemType.DustBoard:
                    return "Dust Board";
                case ItemType.DustSteelPlate:
                    return "Dust Steel Plate";
                case ItemType.FuelConcentrate:
                    return "Fuel Concentrate";
                case ItemType.ImpureWater:
                    return "Impure Water";
                case ItemType.PureWater:
                    return "PureWater";
                case ItemType.SpaceBrick:
                    return "Space Brick";
                case ItemType.Cloth:
                    return "Cloth";
                case ItemType.CulturePaper:
                    return "Culture Paper";
                case ItemType.Seed:
                    return "Seed";
                case ItemType.Fuel:
                    return "Fuel";
                case ItemType.Ice:
                    return "Ice";
            }
            return string.Empty;
        }
    }
}
