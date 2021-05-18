using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BuySell : MonoBehaviour
    {
        private Inventory playerInventory;
        public GameObject player;

        private void Start()
        {
            playerInventory = player.transform.GetChild(0).GetComponent<InventoryController>().ShipInventory;
        }

        public void Sell()
        {
            player.GetComponent<Player>().Credits += playerInventory.Aluminum * 50;
            playerInventory.Aluminum = 0;
        }
    }
}