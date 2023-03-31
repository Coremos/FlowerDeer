using System.Collections.Generic;
using UnityEngine;

namespace FlowerDeer.Utility
{
    public class CustomObjectPool : MonoBehaviour
    {
        private Stack<GameObject> pool = new Stack<GameObject>();
        public GameObject Prefab;

        public GameObject GetInstance()
        {
            GameObject gameInstance;
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

        public void SetInstance(GameObject gameInstance)
        {
            gameInstance.transform.parent = transform;
            gameInstance.gameObject.SetActive(false);
            pool.Push(gameInstance);
        }

        private GameObject CreateInstance()
        {
            return Instantiate(Prefab);
        }
    }
}