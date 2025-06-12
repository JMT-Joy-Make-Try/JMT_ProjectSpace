using System;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingWorker : MonoBehaviour, IBuildingComponent
    {
        public BuildingBase Building { get; private set; }
        public bool IsWorking => _isWorking;
        public event Action<bool> OnWorkingEvent;
        
        protected bool _isWorking;
        
        public void Init(BuildingBase building)
        {
            Building = building;
        }
        
        public virtual void Work()
        {
            if (_isWorking)
            {
                return;
            }

            _isWorking = true;
            OnWorkingEvent?.Invoke(_isWorking);
        }
        
        public virtual void StopWork()
        {
            if (!_isWorking)
            {
                return;
            }

            _isWorking = false;
            OnWorkingEvent?.Invoke(_isWorking);
        }
        
        public void SetWorking(bool isWorking)
        {
            _isWorking = isWorking;
            OnWorkingEvent?.Invoke(_isWorking);
        }
    }
}