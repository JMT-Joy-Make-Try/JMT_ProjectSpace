using System.Collections.Generic;
using System;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SizeLimitQueue<T> : Queue<T>
    {
        private int _maxSize;

        public SizeLimitQueue(int maxSize)
        {
            _maxSize = maxSize;
        }

        public new void Enqueue(T item)
        {
            if (Count >= _maxSize)
            {
                Dequeue();
                throw new InvalidOperationException("Queue size limit exceeded. Item dequeued.");
            }
            base.Enqueue(item);
        }

        public new void Clear()
        {
            base.Clear();
        }
    }
}