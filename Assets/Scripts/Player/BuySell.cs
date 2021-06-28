using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BuySell : MonoBehaviour
    {
        private InventoryController playerInventory;
        public GameObject player;

        private void Start()
        {
            playerInventory = player.transform.GetChild(0).GetComponent<InventoryController>();
        }

        public void Sell(/*Pass the Mineral name and specified price based on planet*/ )
        {
            string item = "Aluminum";
            player.GetComponent<Player>().Credits += playerInventory.GetMineralAmount(item) * 50;
            playerInventory.RemoveAll(item);
        }
    }
}