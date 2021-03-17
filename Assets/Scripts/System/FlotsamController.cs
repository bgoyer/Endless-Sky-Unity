using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.System
{
    public class FlotsamController : MonoBehaviour
    {
        public int MatAmount;
        public string MatType;
        private GameObject lootTip;

        private void Start()
        {
            lootTip = GameObject.Find("/HUD/HideWhenMapIsOpen/Tip");
            Destroy(this.gameObject, 30f);
            MatAmount = 10;
            MatType = "Aluminium";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerShip") || collision.CompareTag("AIShip"))
            {
                StartCoroutine("OnTrigger", collision);
            }
        }

        private IEnumerator OnTrigger(Collider2D col)
        {
            col.GetComponent<InventoryController>().Add(MatType, MatAmount);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            lootTip.GetComponent<Text>().text = $"You looted {MatAmount} {MatType}";
            yield return new WaitForSeconds(3);
            lootTip.GetComponent<Text>().text = "";
            Destroy(this.gameObject);
        }
    }
}