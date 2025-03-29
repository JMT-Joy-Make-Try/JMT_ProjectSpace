using EasySave.Json;
using EasySave.Xml;
using System.Collections.Generic;

namespace JMT.Core.Tool
{
    public static class EasySaveExtension
    {
        public static void ToJson<T>(this T obj, string jsonFileName, bool prettyPrint = false)
        {
            EasyToJson.ToJson(obj, jsonFileName, prettyPrint);
        }
        
        public static T FromJson<T>(this T obj, string jsonFileName)
        {
            return EasyToJson.FromJson<T>(jsonFileName);
        }

        public static void ListToJson<T>(this List<T> list, string jsonFileName, bool prettyPrint = false)
        {
            EasyToJson.ListToJson(list, jsonFileName, prettyPrint);
        }
        
        public static List<T> ListFromJson<T>(this List<T> list, string jsonFileName)
        {
            return EasyToJson.ListFromJson<T>(jsonFileName);
        }
        
        public static void DictionaryToJson<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, string jsonFileName, bool prettyPrint = false)
        {
            EasyToJson.DictionaryToJson(dictionary, jsonFileName, prettyPrint);
        }
        
        public static Dictionary<TKey, TValue> DictionaryFromJson<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, string jsonFileName)
        {
            return EasyToJson.DictionaryFromJson<TKey, TValue>(jsonFileName);
        }

        public static void ToXml<T>(this T obj, string xmlFileName)
        {
            EasyToXml.ToXml(obj, xmlFileName);
        }
        
        public static T FromXml<T>(this T obj, string xmlFileName)
        {
            return EasyToXml.FromXml<T>(xmlFileName);
        }
        
        public static void ListToXml<T>(this List<T> list, string xmlFileName)
        {
            EasyToXml.ListToXml(list, xmlFileName);
        }
        
        public static List<T> ListFromXml<T>(this List<T> list, string xmlFileName)
        {
            return EasyToXml.ListFromXml<T>(xmlFileName);
        }
    }
}