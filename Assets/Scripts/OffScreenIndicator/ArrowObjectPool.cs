using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class ArrowObjectPool : MonoBehaviour
    {
        public static ArrowObjectPool Current;

        [Tooltip("Assign the arrow prefab.")]
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
                Indicator arrow = Instantiate(PooledObject);
                arrow.transform.SetParent(transform, false);
                arrow.Activate(false);
                pooledObjects.Add(arrow);
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
                Indicator arrow = Instantiate(PooledObject);
                arrow.transform.SetParent(transform, false);
                arrow.Activate(false);
                pooledObjects.Add(arrow);
                return arrow;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        private int amountActive;

        public void DeactivateAllPooledObjects()
        {
            amountActive = 0;
            foreach (Indicator box in pooledObjects)
            {
                if (box.isActiveAndEnabled == true)
                {
                    amountActive += 1;
                }

                box.Activate(false);
            }
        }

        public void ActivateAllPooledObjects()
        {
            foreach (Indicator box in pooledObjects)
            {
                if (amountActive > 0)
                {
                    box.Activate(true);
                    amountActive -= 1;
                }
            }
        }
    }
}