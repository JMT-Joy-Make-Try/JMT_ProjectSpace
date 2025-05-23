﻿using UnityEngine;

namespace JMT.Core.Tool.PoolManager.Core
{
    public interface IPoolable
    {
        public PoolingType type { get; set; }
        public GameObject ObjectPrefab { get; }
        
        public void ResetItem();
    }
}