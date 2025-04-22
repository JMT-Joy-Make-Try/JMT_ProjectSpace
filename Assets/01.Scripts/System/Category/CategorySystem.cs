using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public static class CategorySystem
    {
        public static List<T> FilteringCategory<T>(List<T> list, Enum category) where T : ICategorizable
        {
            List<T> result = new();

            for (int i = 0; i < list.Count; i++)
            {
                if (category.Equals(list[i].Category))
                    result.Add(list[i]);
            }
            return result;
        }

        public static List<KeyValuePair<T, int>> FilteringCategory<T>(List<KeyValuePair<T, int>> list, Enum category) where T : ICategorizable
        {
            List<KeyValuePair<T, int>> result = new();

            foreach (var pair in list)
            {
                if (category.Equals(pair.Key.Category))
                    result.Add(pair);
            }

            return result;
        }
    }
}
