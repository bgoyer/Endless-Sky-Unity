using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlotsamController : MonoBehaviour
{
    public int matAmount;
    private string matType = "Aluminum";
    private GameObject LootTip;
    private void Start()
    {
        LootTip = GameObject.Find("/HUD/LootTip");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            StartCoroutine("OnTrigger");
        }

    }

    IEnumerator OnTrigger()
    {
        GameObject.Find("/Player/Inventory").GetComponent<InventoryController>().Aluminum += matAmount;
        LootTip.GetComponent<Text>().text = $"You looted {matAmount} {matType}";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3);
        LootTip.GetComponent<Text>().text = "";
        Destroy(this.gameObject);


    }
}
