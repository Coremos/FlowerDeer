using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlowerDeer.InputSystem
{
    public enum KeyType { LEFT, RIGHT, JUMP };

    public class InputHandler : MonoBehaviour
    {
        private Dictionary<KeyType, KeyCode> keys;
        private KeyValuePair<KeyType, KeyCode>[] keyPairs;
        private int keyLength;
        private static List<IInputHandle> inputHandles = new List<IInputHandle>();

        public static void RegisterInputHandle(IInputHandle inputHandle)
        {
            inputHandles.Add(inputHandle);
        }

        private void Awake()
        {
            InitializeKey();
            UpdateKeyPair();
        }

        private void OnDisable()
        {
            inputHandles.Clear();
        }

        private void InitializeKey()
        {
            keys = new Dictionary<KeyType, KeyCode>();

            keys.Add(KeyType.LEFT, KeyCode.LeftArrow);
            keys.Add(KeyType.RIGHT, KeyCode.RightArrow);
            keys.Add(KeyType.JUMP, KeyCode.Space);
        }

        private void UpdateKeyPair()
        {
            keyPairs = keys.ToArray();
            keyLength = keyPairs.Length;
        }

        private void Update()
        {
            for (int index = 0; index < keyLength; index++)
            {
                if (Input.GetKey(keyPairs[index].Value))
                {
                    for (int handleIndex = 0; handleIndex < inputHandles.Count; handleIndex++)
                    {
                        inputHandles[handleIndex].OnInputKey(keyPairs[index].Key);
                    }
                }

                if (Input.GetKeyUp(keyPairs[index].Value))
                {
                    for (int handleIndex = 0; handleIndex < inputHandles.Count; handleIndex++)
                    {
                        inputHandles[handleIndex].OnInputKeyUp(keyPairs[index].Key);
                    }
                }
            }
        }
    }
}