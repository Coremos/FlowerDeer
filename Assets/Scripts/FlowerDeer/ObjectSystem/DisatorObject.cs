using UnityEngine;

namespace FlowerDeer.ObjectSystem
{
    public class DisatorObject : DropObject
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Drop")) return;
            ParentPool.SetInstance(gameObject);
        }
    }
}