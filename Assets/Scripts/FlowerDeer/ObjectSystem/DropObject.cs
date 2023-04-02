using FlowerDeer.Utility;
using UnityEngine;

namespace FlowerDeer.ObjectSystem
{
    public enum DropObjectType { DISATOR, FLOWER };

    public class DropObject : MonoBehaviour
    {
        public int Score;
        public int HPChange;
        public float SpawnPercentage;
        public DropObjectType ObjectType;
        public CustomObjectPool ParentPool;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Drop")) return;
            ParentPool.SetInstance(gameObject);
        }
    }
}