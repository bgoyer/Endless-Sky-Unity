using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    public class BoxObjectPool : MonoBehaviour
    {
        public static BoxObjectPool Current;

        [Tooltip("Assign the box prefab.")]
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
                Indicator box = Instantiate(PooledObject);
                box.transform.SetParent(transform, false);
                box.Activate(false);
                pooledObjects.Add(box);
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
                Indicator box = Instantiate(PooledObject);
                box.transform.SetParent(transform, false);
                box.Activate(false);
                pooledObjects.Add(box);
                return box;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator box in pooledObjects)
            {
                box.Activate(false);
            }
        }
    }
}