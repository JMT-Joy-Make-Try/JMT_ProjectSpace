using System.Collections.Generic;
using System;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SizeLimitQueue<T> : SerializeQueue<T>
    {
        private int _maxSize;

        public SizeLimitQueue(int maxSize)
        {
            _maxSize = maxSize;
        }

        public new void Enqueue(T item)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("Queue size limit exceeded.");
            }
            base.Enqueue(item);
        }

        public new void Clear()
        {
            base.Clear();
        }

        public bool IsFull() => Count >= _maxSize;
    }
}