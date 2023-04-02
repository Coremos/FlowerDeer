using FlowerDeer.Manager;
using FlowerDeer.ObjectSystem;
using UnityEngine;

namespace FlowerDeer
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionHandler : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;
        private ICollisionHandle collisionHandle;
        private Player player;

        private void Awake()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            TryGetComponent(out rigidbody);
            TryGetComponent(out collisionHandle);
            TryGetComponent(out player);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                collisionHandle.OnTouchedGround();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Drop"))
            {
                collision.transform.TryGetComponent<DropObject>(out var drop);
                Proceed(drop);
            }
        }

        private void Proceed(DropObject dropObject)
        {
            player.ModifyHP(dropObject.HPChange);
            UIManager.Instance.ShowScoreIndicator(dropObject.transform.position, dropObject.Score);
            GameManager.Instance.Score += dropObject.Score;
            SoundEffectManager.Instance.PlaySound("GetPoint");

            //switch (dropObject.ObjectType)
            //{
            //    case DropObjectType.DISATOR:
            //        var disatorObject = dropObject as DisatorObject;
            //        break;
            //    case DropObjectType.FLOWER:
            //        var flower = dropObject as FlowerObject;
            //        break;
            //}
        }
    }
}