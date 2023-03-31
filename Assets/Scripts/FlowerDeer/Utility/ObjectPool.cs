using System.Collections.Generic;
using UnityEngine;

namespace FlowerDeer.Utility
{
    public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : Component
    {
        private Stack<T> pool = new Stack<T>();

        public T GetInstance()
        {
            T gameInstance;
            if (pool.Count > 0)
            {
                gameInstance = pool.Pop();
            }
            else
            {
                gameInstance = CreateInstance();
            }
            gameInstance.transform.parent = null;
            gameInstance.gameObject.SetActive(true);
            return gameInstance;
        }

        public void SetInstance(T gameInstance)
        {
            gameInstance.transform.parent = transform;
            gameInstance.gameObject.SetActive(false);
            pool.Push(gameInstance);
        }

        private T CreateInstance()
        {
            var gameObject = new GameObject();
            gameObject.name = typeof(T).Name;
            return gameObject.AddComponent<T>();
        }
    }
}