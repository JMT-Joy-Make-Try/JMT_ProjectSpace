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
        private bool _isCrafting = false;
        private float _craftProgress = 0f;
        private CreateItemSO _currentRecipe;

        public event Action<ItemSO> OnCraftComplete;

        protected override void Start()
        {
            base.Start();
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
}