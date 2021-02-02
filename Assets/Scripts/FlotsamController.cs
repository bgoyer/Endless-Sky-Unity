using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FlotsamController : MonoBehaviour
    {
        public int matAmount;
        public string matType;
        private GameObject LootTip;
        private void Start()
        {
            LootTip = GameObject.Find("/HUD/Tip");
            Destroy(this.gameObject, 30f);
            matAmount = 10;
            matType = "Aluminium";
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
            GameObject.Find("/Player").transform.GetChild(0).GetComponent<InventoryController>().Add(matType, matAmount);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            LootTip.GetComponent<Text>().text = $"You looted {matAmount} {matType}";
            yield return new WaitForSeconds(3);
            LootTip.GetComponent<Text>().text = "";
            Destroy(this.gameObject);


        }
    }
}
