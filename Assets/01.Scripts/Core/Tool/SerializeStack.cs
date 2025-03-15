using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SerializeStack<T> : IEnumerable<T>
    {
        [SerializeField] private List<T> items = new List<T>();
        
        public void Push(T item)
        {
            items.Add(item);
        }
        
        public T Pop()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }
            var item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }
        
        public T Peek()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }
            return items[items.Count - 1];
        }
        
        public void Clear()
        {
            items.Clear();
        }
        
        public int Count => items.Count;
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}