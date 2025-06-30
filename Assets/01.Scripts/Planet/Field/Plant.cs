using JMT.DayTime;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Planets.Field
{
    public class Plant : MonoBehaviour
    {
        [Tooltip("몇분에 1단계씩 자라는지"), SerializeField] private TimeData _timeData;
        
        [SerializeField] private GameObject[] _plantObjects;
        
        private GameObject _currentPlantObject;
        private int _growthStage = 0;

        private bool _isGrowEnd;
        
        public bool IsGrowEnd => _isGrowEnd;

        private void Awake()
        {
            foreach (var plant in _plantObjects)
            {
                plant.SetActive(false);
            }
            
            _plantObjects[0].SetActive(true);
            _currentPlantObject = _plantObjects[0];
        }

        private void Start()
        {
            StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            while (!_isGrowEnd)
            {
                yield return new WaitForSeconds(_timeData.GetSecond());
                Grow();
            }
        }

        private void Grow()
        {
            _growthStage++;
            if (_growthStage >= _plantObjects.Length)
            {
                Debug.Log("Plant is fully grown.");
                _isGrowEnd = true;
                return;
            }
            
            ChangePlantObject();
        }

        private void ChangePlantObject()
        {
            
            _currentPlantObject.SetActive(false);
            if (_plantObjects[0].activeSelf == false) 
            {
                _plantObjects[0].SetActive(true);
            }
            _currentPlantObject = _plantObjects[_growthStage];
            if (_currentPlantObject != null)
            {
                _currentPlantObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No plant object found for the current growth stage.");
            }
        }
    }
}