using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BuySell : MonoBehaviour
    {
        private Inventory playerInventory;
        private Player player;
        private void Start()
        {
            playerInventory = GameObject.Find("/Player/Inventory").GetComponent<InventoryController>().ShipInventory;
            player = GameObject.Find("/Player").GetComponent<Player>();
        }
        public void Sell()
        {
            player.Credits += playerInventory.Aluminum * 50;
            playerInventory.Aluminum = 0;
        
        }
    }
}
