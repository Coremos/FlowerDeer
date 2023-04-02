using UnityEngine;

namespace FlowerDeer.ObjectSystem
{
    public class FlowerObject : DropObject
    {
        private Rigidbody2D rigid;

        private void Awake()
        {
            TryGetComponent(out rigid);
        }

        private void OnEnable()
        {
            rigid.simulated = true;
            rigid.isKinematic = false;
        }

        private void StickToPlayer(Transform targetTransform)
        {
            rigid.simulated = false;
            rigid.isKinematic = true;
            transform.parent = targetTransform;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Drop")) return;
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                StickToPlayer(collision.transform);
                return;
            }
            ParentPool.SetInstance(gameObject);
        }
    }
}