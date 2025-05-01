using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System.Text;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Tool
{
    public static class ObjectExtension
    {
        public static string GetName(ItemType key)
        {
            string name = Enum.GetName(key.GetType(), key);
            if (string.IsNullOrEmpty(name)) return string.Empty;

            var result = new StringBuilder();
            result.Append(name[0]);

            for (int i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                    result.Append(' ');

                result.Append(name[i]);
            }

            return result.ToString();
        }

        public static bool Contains<T, TU>(this List<SerializeTuple<T, TU>> tuple, T item1)
        {
            foreach (var t in tuple)
            {
                if (t.Item1.Equals(item1))
                    return true;
            }

            return false;
        }

        public static Vector3 GetRandomNearestPosition(this Vector3 vector, float radius)
        {
            float x = UnityEngine.Random.Range(vector.x - radius, vector.x + radius);
            float z = UnityEngine.Random.Range(vector.z - radius, vector.z + radius);

            return new Vector3(x, vector.y, z);
        }
    }
}