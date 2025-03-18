using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Tool.PoolManager.Core
{
    public class PoolingTableSO : ScriptableObject
    {
        public List<PoolingItemSO> datas = new List<PoolingItemSO>();
    }
}