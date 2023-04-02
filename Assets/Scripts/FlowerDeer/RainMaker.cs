using FlowerDeer.Manager;
using FlowerDeer.ObjectSystem;
using FlowerDeer.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerDeer
{
    public class RainMaker : Singleton<RainMaker>
    {

        [SerializeField] private List<DropObject> prefabs = new List<DropObject>();

        [Range(-Constants.WIDTH_HALF, Constants.WIDTH_HALF)]
        [SerializeField] private float xMin;

        [Range(-Constants.WIDTH_HALF, Constants.WIDTH_HALF)]
        [SerializeField] private float xMax;

        [Range(0.0f, 10.0f)]
        [SerializeField] private float y;

        [Range(0.0001f, 10.0f)]
        [SerializeField] private float instantiateDelay = 1.5f;

        private Dictionary<DropObject, CustomObjectPool> pools;
        [SerializeField] private float[] spawnPercentageLookupTable;

        [SerializeField] private Player player;

        protected override void Awake()
        {
            base.Awake();

            InitializePools();
            InitializeLUT();
        }

        private void Start()
        {
            OnTimerDone();
            LevelUpdater();
        }

        private void LevelUpdater()
        {
            instantiateDelay *= instantiateDelay * 0.9f;
            if (instantiateDelay <= 0)
            {
                instantiateDelay = 0.0001f;
                return;
            }
            player.UpdateLoseHPAmount();
            TimerManager.Instance.StartTimer(30.0f, LevelUpdater);
        }

        private void InitializePools()
        {
            pools = new Dictionary<DropObject, CustomObjectPool>();
            for (int index = 0; index < prefabs.Count; index++)
            {
                var gameObject = new GameObject();
                var pool = gameObject.AddComponent<CustomObjectPool>();
                pool.transform.parent = transform;
                pool.Prefab = prefabs[index].gameObject;
                pool.name = string.Concat(pool.Prefab.name, "Pool");
                pools.Add(prefabs[index], pool);
            }
        }

        private void OnTimerDone()
        {
            SpawnRain();
            TimerManager.Instance.StartTimer(instantiateDelay, OnTimerDone);
        }

        private void InitializeLUT()
        {
            float totalPercentage = 0.0f;
            float previousPercentage = 0.0f;
            float totalDivider;
            for (int index = 0; index < prefabs.Count; index++)
            {
                totalPercentage += prefabs[index].SpawnPercentage;
            }
            totalDivider = 1.0f / totalPercentage;

            spawnPercentageLookupTable = new float[prefabs.Count - 1];
            for (int index = 0; index < prefabs.Count - 1; index++)
            {
                previousPercentage = spawnPercentageLookupTable[index] = previousPercentage + prefabs[index].SpawnPercentage * totalDivider;
            }
        }

        private DropObject GetRandomDropObject()
        {
            var dropObject = prefabs[prefabs.Count - 1];
            float randomValue = Random.Range(0.0f, 1.0f);
            for (int index = 0; index < spawnPercentageLookupTable.Length; index++)
            {
                if (randomValue <= spawnPercentageLookupTable[index])
                {
                    dropObject = prefabs[index];
                    break;
                }
            }
            return dropObject;
        }

        private void SpawnRain()
        {
            var dropObjectPrefab = GetRandomDropObject();
            var dropObject = pools[dropObjectPrefab].GetInstance();
            dropObject.transform.parent = transform;
            dropObject.transform.position = GetRandomHorizontalValue();
            dropObject.TryGetComponent<DropObject>(out var drop);
            drop.ParentPool = pools[dropObjectPrefab];
        }

        private Vector2 GetRandomHorizontalValue()
        {
            var position = new Vector2();
            position.x = Random.Range(xMin, xMax);
            position.y = y;
            return position;
        }
    }
}