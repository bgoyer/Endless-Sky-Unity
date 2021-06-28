using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class OutpostObjectPool : MonoBehaviour
    {
        public static OutpostObjectPool Current;

        [Tooltip("Assign the outpost prefab.")]
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
                Indicator outpost = Instantiate(PooledObject);
                outpost.transform.SetParent(transform, false);
                outpost.Activate(false);
                pooledObjects.Add(outpost);
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
                Indicator outpost = Instantiate(PooledObject);
                outpost.transform.SetParent(transform, false);
                outpost.Activate(false);
                pooledObjects.Add(outpost);
                return outpost;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator outpost in pooledObjects)
            {
                outpost.Activate(false);
            }
        }
    }
}