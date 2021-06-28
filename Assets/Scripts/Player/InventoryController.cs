using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class InventoryController : MonoBehaviour
    {
        public Inventory ShipInventory = new Inventory();

        public int GetMineralAmount(string item)
        {
            return ShipInventory.Minerals.Where(kbp => kbp.Key == item).Select(kbp => kbp.Value).FirstOrDefault();
        }

        public void Add(string item, int amount)
        {
            if (true)
            {
                int mineralAmount = ShipInventory.Minerals.Where(kbp => kbp.Key == item).Select(kbp => kbp.Value).FirstOrDefault();
                mineralAmount += amount;
                ShipInventory.Minerals[item] = mineralAmount;
            }
        }

        public void RemoveAll(string item)
        {
            int mineralAmount = ShipInventory.Minerals.Where(kbp => kbp.Key == item).Select(kbp => kbp.Value).FirstOrDefault();
            mineralAmount = 0;
            ShipInventory.Minerals[item] = mineralAmount;
        }
    }

    public class Inventory
    {
        public Dictionary<string, int> Minerals = new Dictionary<string, int>()
        {
            ["Aluminum"] = 0
        };

        public List<string> SectorMaps = new List<string> { };
    }
}