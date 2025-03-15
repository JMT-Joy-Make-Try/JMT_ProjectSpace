namespace JMT.Core.Tool
{
    public static class ObjectExtension
    {
        public static bool Contain<T>(this object obj, T value)
        {
            return obj is T t && t.Equals(value);
        }
    }
}