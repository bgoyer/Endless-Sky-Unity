using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class PlanetObjectPool : MonoBehaviour
    {
        public static PlanetObjectPool current;

        [Tooltip("Assign the planet prefab.")]
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
                Indicator planet = Instantiate(pooledObject);
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
            if (willGrow)
            {
                Indicator planet = Instantiate(pooledObject);
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