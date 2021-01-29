using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public Inventory PlayerInventory = new Inventory();

    public void Add(String Item, int Amount)
    {
        if (Item == "Alunimum")
        {
            PlayerInventory.Aluminum += Amount;
        }
    }
}
public class Inventory
{
    public int Aluminum;
    public List<string> SectorMaps = new List<string> { };
}