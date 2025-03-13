using UnityEngine;

namespace JMT.Core.Tool
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Get component or add component if it does not exist.
        /// </summary>
        /// <param name="gameObject">본인</param>
        /// <typeparam name="T">컴포넌트 타입</typeparam>
        /// <returns>받은 컴포넌트</returns>
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

        public static GameObject GetRootGameObject(this GameObject gameObject)
        {
            return gameObject.transform.root.gameObject;
        }
    }
}