using FlowerDeer.InputSystem;
using System;
using UnityEngine;

namespace FlowerDeer
{
    public class Player : MonoBehaviour, IInputHandle, ICollisionHandle
    {
        private enum DirectionType { LEFT, RIGHT };

        private const float MOVESPEED_MIN = 0.1f;
        private const float MOVESPEED_MAX = 10.0f;

        [Range(MOVESPEED_MIN, MOVESPEED_MAX)]
        [SerializeField] private float moveSpeed;

        private float hp;
        private float hpMax;

        private void Awake()
        {
            RegisterInputHandle();
            InitializeComponents();
        }

        private void RegisterInputHandle()
        {
            InputHandler.RegisterInputHandle(this);
        }

        private void InitializeComponents()
        {
        }

        public void OnInputKey(KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.LEFT:
                    Move(DirectionType.LEFT);
                    break;
                case KeyType.RIGHT:
                    Move(DirectionType.RIGHT);
                    break;
            }
        }

        private void Move(DirectionType direction)
        {
            var movedPosition = transform.position;
            if (direction == DirectionType.LEFT)
            {
                movedPosition.x -= moveSpeed * Time.deltaTime;
            }
            else if (direction == DirectionType.RIGHT)
            {
                movedPosition.x += moveSpeed * Time.deltaTime;
            }

            transform.position = movedPosition;
        }

        public void OnInputKeyDown(KeyType keyType)
        {
        }

        public void OnInputKeyUp(KeyType keyType)
        {
        }

        private void FixedUpdate()
        {
            if (hp > hpMax)
            {
                hp = hpMax;
            }
            UIManager.Instance.SetHP(hp);
        }

        public void OnTouchedDamageObject(float value)
        {
            hp -= value;
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (hp <= 0)
            {
                OnDie();
            }
        }

        private void OnDie()
        {

        }
    }

}