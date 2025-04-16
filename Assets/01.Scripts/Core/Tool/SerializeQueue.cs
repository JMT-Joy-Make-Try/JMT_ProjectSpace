using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SerializeQueue<T> : IEnumerable<T>
    {
        [SerializeField] private List<T> items = new List<T>();
        
        public List<T> Items => items;
        
        public int Count => items.Count;
        
        public void Enqueue(T item)
        {
            items.Add(item);
        }
        
        public T Dequeue()
        {
            if (items.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty");
            }
            T item = items[0];
            items.RemoveAt(0);
            return item;
        }
        
        public T Peek()
        {
            if (items.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty");
            }
            return items[0];
        }
        
        public void Clear()
        {
            items.Clear();
        }
        
        public bool Contains(T item)
        {
            return items.Contains(item);
        }
        
        public void Remove(T item)
        {
            items.Remove(item);
        }
        
        public T[] ToArray()
        {
            return items.ToArray();
        }
        
        public List<T> ToList()
        {
            return items;
        }

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