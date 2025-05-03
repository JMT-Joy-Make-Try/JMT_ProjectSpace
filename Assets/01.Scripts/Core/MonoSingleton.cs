    using System;
    using UnityEngine;

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static bool HasInstance = _instance != null;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        Debug.LogWarning($"{typeof(T).Name} instance not found in the scene.");
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }
    }