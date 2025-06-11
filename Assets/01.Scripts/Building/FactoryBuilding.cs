using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class FactoryBuilding : BuildingBase
    {
        [SerializeField] private List<CreateItemSO> _craftableItems = new();
        [SerializeField] private FactoryBuildingType _factoryBuildingType = FactoryBuildingType.None;

        private bool _isCrafting = false;
        private float _craftProgress = 0f;
        private CreateItemSO _currentRecipe;

        public event Action<ItemSO> OnCraftComplete;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCraft(_craftableItems[0]);
            }
        }

        private void Start()
        {
            BuildingUIManager.Instance.FactoryCompo.factoryBuilding = this;
        }

        public bool StartCraft(CreateItemSO recipe)
        {
            if (_isCrafting)
                return false;

            _currentRecipe = recipe;
            _isCrafting = true;
            _craftProgress = 0f;
            StartCoroutine(CraftCoroutine(recipe.CreateTime.GetSecond()));
            return true;
        }

        private IEnumerator CraftCoroutine(float duration)
        {
            while (_craftProgress < duration)
            {
                _craftProgress += Time.deltaTime;
                yield return null;
            }
            CompleteCraft();
        }

        private void CompleteCraft()
        {
            _isCrafting = false;
            var item = _currentRecipe.ResultItem;
            OnCraftComplete?.Invoke(item);
            BuildingUIManager.Instance.StorageCompo.AddItem(item, 1);
        }
    }

    public enum FactoryBuildingType
    {
        None = 0,
        Equipment = 1, // 장비 제작소
        Resource = 2, // 자원 제작소
    }
}