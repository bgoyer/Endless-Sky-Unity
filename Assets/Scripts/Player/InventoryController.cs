using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public Inventory PlayerInventory = new Inventory();

    private void Update()
    {

    }
}
public class Inventory
{
    public int Aluminum;
    public List<string> SectorMaps = new List<string> { };
}