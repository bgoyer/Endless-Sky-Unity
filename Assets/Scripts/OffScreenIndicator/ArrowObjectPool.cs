using System.Collections.Generic;
using UnityEngine;

internal class ArrowObjectPool : MonoBehaviour
{
    public static ArrowObjectPool current;

    [Tooltip("Assign the arrow prefab.")]
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
            Indicator arrow = Instantiate(pooledObject);
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
        if (willGrow)
        {
            Indicator arrow = Instantiate(pooledObject);
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