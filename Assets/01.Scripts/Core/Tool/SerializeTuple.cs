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
            else if (tuple.Item2 is double c)
            {
                c--;
                tuple.Item2 = (TU) (object) c;
            }
            else if (tuple.Item2 is long d)
            {
                d--;
                tuple.Item2 = (TU) (object) d;
            }
            else if (tuple.Item2 is short e)
            {
                e--;
                tuple.Item2 = (TU) (object) e;
            }
            else if (tuple.Item2 is byte f)
            {
                f--;
                tuple.Item2 = (TU) (object) f;
            }
            else if (tuple.Item2 is sbyte g)
            {
                g--;
                tuple.Item2 = (TU) (object) g;
            }
            else if (tuple.Item2 is uint h)
            {
                h--;
                tuple.Item2 = (TU) (object) h;
            }
            else if (tuple.Item2 is ushort i)
            {
                i--;
                tuple.Item2 = (TU) (object) i;
            }
            else if (tuple.Item2 is ulong j)
            {
                j--;
                tuple.Item2 = (TU) (object) j;
            }
            else if (tuple.Item2 is decimal k)
            {
                k--;
                tuple.Item2 = (TU) (object) k;
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