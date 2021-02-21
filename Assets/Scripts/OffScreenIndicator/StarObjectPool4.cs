using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class EnemyOutpostObjectPool : MonoBehaviour
    {
        public static EnemyOutpostObjectPool Current;

        [Tooltip("Assign the enemy outpost prefab.")]
        public Indicator PooledObject;

        [Tooltip("Initial pooled amount.")]
        public int PooledAmount = 1;

        [Tooltip("Should the pooled amount increase.")]
        public bool WillGrow = true;

        private List<Indicator> pooledObjects;

        private void Awake()
        {
            Current = this;
        }

        private void Start()
        {
            pooledObjects = new List<Indicator>();

            for (int i = 0; i < PooledAmount; i++)
            {
                Indicator enemyOutpost = Instantiate(PooledObject);
                enemyOutpost.transform.SetParent(transform, false);
                enemyOutpost.Activate(false);
                pooledObjects.Add(enemyOutpost);
            }
        }

        /// <summary>
        /// Gets pooled objects from the pool.
        /// </summary>
        /// <returns></returns>
        public Indicator GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].Active)
                {
                    return pooledObjects[i];
                }
            }
            if (WillGrow)
            {
                Indicator enemyOutpost = Instantiate(PooledObject);
                enemyOutpost.transform.SetParent(transform, false);
                enemyOutpost.Activate(false);
                pooledObjects.Add(enemyOutpost);
                return enemyOutpost;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator enemyOutpost in pooledObjects)
            {
                enemyOutpost.Activate(false);
            }
        }
    }
}