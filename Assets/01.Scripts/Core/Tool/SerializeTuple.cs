using System;

namespace JMT.Core.Tool
{
    [Serializable]
    public class SerializeTuple <T, TU>
    {
        public T Item1;
        public TU Item2;
        
        public static SerializeTuple<T, TU>  operator-- (SerializeTuple<T, TU> tuple)
        {
            if (tuple.Item2 is int a)
            {
                a--;
                tuple.Item2 = (TU) (object) a;
            }
            else if (tuple.Item2 is float b)
            {
                b--;
                tuple.Item2 = (TU) (object) b;
            }
            else
            {
                throw new InvalidOperationException("Unsupported type");
            }
            return tuple;
        }
        
        public SerializeTuple(T item1, TU item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}