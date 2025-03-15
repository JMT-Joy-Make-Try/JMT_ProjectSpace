using System;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SerializeTuple <T, TU>
    {
        public T Item1;
        public TU Item2;
        
        public SerializeTuple(T item1, TU item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}