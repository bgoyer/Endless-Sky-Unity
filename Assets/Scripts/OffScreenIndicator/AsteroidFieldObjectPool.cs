using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class AsteroidFieldObjectPool : MonoBehaviour
    {
        public static AsteroidFieldObjectPool current;

        [Tooltip("Assign the asteriod field prefab.")]
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
                Indicator asteroidField = Instantiate(pooledObject);
                asteroidField.transform.SetParent(transform, false);
                asteroidField.Activate(false);
                pooledObjects.Add(asteroidField);
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
                Indicator asteroidField = Instantiate(pooledObject);
                asteroidField.transform.SetParent(transform, false);
                asteroidField.Activate(false);
                pooledObjects.Add(asteroidField);
                return asteroidField;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator asteroidField in pooledObjects)
            {
                asteroidField.Activate(false);
            }
        }
    }
}