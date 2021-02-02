using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class EnemyOutpostObjectPool : MonoBehaviour
    {
        public static EnemyOutpostObjectPool current;

        [Tooltip("Assign the enemy outpost prefab.")]
        public Indicator pooledObject;

        [Tooltip("Initial pooled amount.")]
        public int pooledAmount = 1;

        [Tooltip("Should the pooled amount increase.")]
        public bool willGrow = true;

        private List<Indicator> pooledObjects;

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            pooledObjects = new List<Indicator>();

            for (int i = 0; i < pooledAmount; i++)
            {
                Indicator enemyOutpost = Instantiate(pooledObject);
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
            if (willGrow)
            {
                Indicator enemyOutpost = Instantiate(pooledObject);
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