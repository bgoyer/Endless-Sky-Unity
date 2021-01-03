using System.Collections.Generic;
using UnityEngine;

internal class OutpostObjectPool : MonoBehaviour
{
    public static OutpostObjectPool current;

    [Tooltip("Assign the outpost prefab.")]
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
            Indicator outpost = Instantiate(pooledObject);
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
        if (willGrow)
        {
            Indicator outpost = Instantiate(pooledObject);
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