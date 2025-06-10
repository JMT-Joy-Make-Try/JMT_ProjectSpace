using JMT.Item;
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

        //private bool _isHold;

        public event Action<ItemSO> OnCraftComplete;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCraft(_craftableItems[0]);
            }
        }

        public bool StartCraft(CreateItemSO recipe)
        {
            if (_isCrafting || !_craftableItems.Contains(recipe))
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
                //if (_isHold)
                    _craftProgress += Time.deltaTime;
                yield return null;
            }
            CompleteCraft();
        }

        // public void SetHold(bool isHold)
        // {
        //     _isHold = isHold;
        // }

        private void CompleteCraft()
        {
            _isCrafting = false;
            var item = _currentRecipe.ResultItem;
            OnCraftComplete?.Invoke(item);
            // 아이템을 건물 앞에 생성하는 로직 추가
        }
    }

    public enum FactoryBuildingType
    {
        None = 0,
        Equipment = 1, // 장비 제작소
        Resource = 2, // 자원 제작소
    }
}