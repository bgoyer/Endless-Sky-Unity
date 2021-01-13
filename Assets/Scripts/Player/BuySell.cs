using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySell : MonoBehaviour
{
    private Inventory PlayerInventory;
    private Player player;
    private void Start()
    {
        PlayerInventory = GameObject.Find("/Player/Inventory").GetComponent<InventoryController>().PlayerInventory;
        player = GameObject.Find("/Player").GetComponent<Player>();
    }
    public void Sell()
    {
        player.Credits += PlayerInventory.Aluminum * 50;
        PlayerInventory.Aluminum = 0;
        
    }
}
