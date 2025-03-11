using UnityEngine;

namespace JMT.Core.Tool
{
    public static class GameObjectExtension
    {
        public static T GetComponentOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.GetComponentInChildren<T>();
                if (component == null)
                {
                    Debug.LogError($"There is no {typeof(T).Name} component in {gameObject.name}");
                    component = gameObject.AddComponent<T>();
                }
            }
            
            return component;
        }
    }
}