using System.Collections.Generic;
using UnityEngine;

internal class StarObjectPool : MonoBehaviour
{
    public static StarObjectPool current;

    [Tooltip("Assign the star prefab.")]
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
            Indicator star = Instantiate(pooledObject);
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
        if (willGrow)
        {
            Indicator star = Instantiate(pooledObject);
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