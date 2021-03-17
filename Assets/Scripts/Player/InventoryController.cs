using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class InventoryController : MonoBehaviour
    {
        public Inventory ShipInventory = new Inventory();

        public void Add(String item, int amount)
        {
            if (item == "Alunimum")
            {
                ShipInventory.Aluminum += amount;
            }
        }
    }

    public class Inventory
    {
        public int Aluminum;
        public List<string> SectorMaps = new List<string> { };
    }
}