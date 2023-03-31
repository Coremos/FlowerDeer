using UnityEngine;

namespace FlowerDeer.Utility
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var gameObject = new GameObject();
                        gameObject.name = typeof(T).Name;
                        instance = gameObject.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        private static T instance;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
