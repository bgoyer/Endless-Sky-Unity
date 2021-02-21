using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class PlanetObjectPool : MonoBehaviour
    {
        public static PlanetObjectPool Current;

        [Tooltip("Assign the planet prefab.")]
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
                Indicator planet = Instantiate(PooledObject);
                planet.transform.SetParent(transform, false);
                planet.Activate(false);
                pooledObjects.Add(planet);
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
                Indicator planet = Instantiate(PooledObject);
                planet.transform.SetParent(transform, false);
                planet.Activate(false);
                pooledObjects.Add(planet);
                return planet;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator planet in pooledObjects)
            {
                planet.Activate(false);
            }
        }
    }
}