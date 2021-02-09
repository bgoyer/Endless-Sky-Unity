using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class InventoryController : MonoBehaviour
    {

        public Inventory ShipInventory = new Inventory();

        public void Add(String Item, int Amount)
        {
            if (Item == "Alunimum")
            {
                ShipInventory.Aluminum += Amount;
            }
        }
    }
    public class Inventory
    {
        public int Aluminum;
        public List<string> SectorMaps = new List<string> { };
    }
}