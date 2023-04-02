using FlowerDeer.InputSystem;
using FlowerDeer.Manager;
using System;
using UnityEngine;

namespace FlowerDeer
{
    public class Player : MonoBehaviour, IInputHandle, ICollisionHandle
    {
        private enum DirectionType { LEFT, RIGHT };

        private const float MOVESPEED_MIN = 0.1f;
        private const float MOVESPEED_MAX = 10.0f;
        private const float SIDE = 4.0f;
        private const float HP_LOSE_AMOUNT_MULTIPLIER = 1.2f;

        [Range(MOVESPEED_MIN, MOVESPEED_MAX)]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpPower;

        private float hp = 100.0f;
        private float hpMax = 100.0f;
        private Animator animator;
        private new Rigidbody2D rigidbody;
        private bool canJump;
        private Coroutine hpLoser;
        private float hpLoseAmount = 1.0f;

        private void Awake()
        {
            RegisterInputHandle();
            InitializeComponents();
        }

        private void OnEnable()
        {
            hpLoser = TimerManager.Instance.StartTimer(1.0f, LoseHP);
        }

        private void OnDisable()
        {
            TimerManager.Instance.CancleTimer(hpLoser);
        }

        private void LoseHP()
        {
            TimerManager.Instance.CancleTimer(hpLoser);
            hp -= hpLoseAmount;
            UpdateHP();
            hpLoser = TimerManager.Instance.StartTimer(1.0f, LoseHP);
        }

        public void UpdateLoseHPAmount()
        {
            hpLoseAmount *= HP_LOSE_AMOUNT_MULTIPLIER;
        }

        private void RegisterInputHandle()
        {
            InputHandler.RegisterInputHandle(this);
        }

        private void InitializeComponents()
        {
            TryGetComponent(out animator);
            TryGetComponent(out rigidbody);
        }

        private bool isLeftDown;
        private bool isRightDown;

        public void OnInputKey(KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.LEFT:
                    Move(DirectionType.LEFT);
                    isLeftDown = true;
                    break;
                case KeyType.RIGHT:
                    Move(DirectionType.RIGHT);
                    isRightDown = true;
                    break;
                case KeyType.JUMP:
                    Jump();
                    break;
            }
        }

        private void Jump()
        {
            if (!canJump) return;
            canJump = false;
            animator.Play("Jump");
            rigidbody.AddForce(Vector2.up * jumpPower);
        }

        private void Move(DirectionType direction)
        {
            var movedPosition = transform.position;
            if (direction == DirectionType.LEFT)
            {
                movedPosition.x -= moveSpeed * Time.deltaTime;
                transform.localScale = new Vector2(-1.0f, 1.0f);
            }
            else if (direction == DirectionType.RIGHT)
            {
                movedPosition.x += moveSpeed * Time.deltaTime;
                transform.localScale = new Vector2(1.0f, 1.0f);
            }

            transform.position = movedPosition;
        }

        public void OnInputKeyDown(KeyType keyType)
        {
        }

        public void OnInputKeyUp(KeyType keyType)
        {
            if (keyType == KeyType.LEFT)
            {
                isLeftDown = false;
            }
            if (keyType == KeyType.RIGHT)
            {
                isRightDown = false;
            }
        }

        private void FixedUpdate()
        {
            UIManager.Instance.SetHPPercentage(hp / hpMax);
            if (isLeftDown | isRightDown)
            {
                animator.SetBool("Move", true);
            }
            else if (!isLeftDown && !isRightDown)
            {
                animator.SetBool("Move", false);
            }
            UpdateSide();
        }

        private void UpdateSide()
        {
            var tempPosition = transform.position;
            if (transform.position.x < -SIDE)
            {
                tempPosition.x = SIDE;
            }
            if (transform.position.x > SIDE)
            {
                tempPosition.x = -SIDE;
            }
            transform.position = tempPosition;
        }

        public void ModifyHP(float value)
        {
            hp += value;
            UpdateHP();
        }

        public void UpdateHP()
        {
            CheckMax();
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (hp <= 0)
            {
                OnDie();
            }
        }
        
        private void CheckMax()
        {
            if (hp > hpMax)
            {
                hp = hpMax;
            }
        }

        private void OnDie()
        {
            SoundEffectManager.Instance.PlaySound("Fanfare");
            GameManager.Instance.GameOver();
        }

        public void OnTouchedGround()
        {
            canJump = true;
        }

        public void OnTouchedDamageObject(float value)
        {
            throw new NotImplementedException();
        }
    }

}