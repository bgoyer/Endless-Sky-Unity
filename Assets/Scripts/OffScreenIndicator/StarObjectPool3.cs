﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    internal class StarObjectPool : MonoBehaviour
    {
        public static StarObjectPool Current;

        [Tooltip("Assign the star prefab.")]
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
                Indicator star = Instantiate(PooledObject);
                star.transform.SetParent(transform, false);
                star.Activate(false);
                pooledObjects.Add(star);
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
                Indicator star = Instantiate(PooledObject);
                star.transform.SetParent(transform, false);
                star.Activate(false);
                pooledObjects.Add(star);
                return star;
            }
            return null;
        }

        /// <summary>
        /// Deactive all the objects in the pool.
        /// </summary>
        public void DeactivateAllPooledObjects()
        {
            foreach (Indicator star in pooledObjects)
            {
                star.Activate(false);
            }
        }
    }
}