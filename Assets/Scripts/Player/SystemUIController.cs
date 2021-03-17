using Assets.Scripts.OffScreenIndicator;
using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SystemUIController : MonoBehaviour
    {
        public GameObject LocationText;
        public GameObject SystemUi;
        private string locName;
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("System") || collision.gameObject.CompareTag("AsteriodField"))
            {
                if (collision.gameObject.CompareTag("AsteriodField"))
                {
                    if (!player.transform.transform.GetChild(0).GetComponent<ShipVariables>().CanControl)
                    {
                        return;
                    }
                }
                locName = collision.gameObject.name.ToUpper();
                for (var body = 0; body < collision.transform.childCount; body++)
                {
                    if (collision.transform.GetChild(body).GetComponent<Target>() != null)
                    {
                        collision.transform.GetChild(body).GetComponent<Target>().enabled = true;
                    }
                }
                transform.GetComponent<ShipVariables>().UpdateShip();
                StartCoroutine(OnTrigger(collision));
                SystemUi.GetComponent<Text>().text = collision.transform.name;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("System") || collision.gameObject.CompareTag("AsteriodField"))
            {
                locName = collision.transform.parent.gameObject.name.ToUpper();
                StartCoroutine(OnTrigger(collision));
                SystemUi.GetComponent<Text>().text = collision.transform.parent.gameObject.name;
                for (var body = 0; body < collision.transform.childCount; body++)
                {
                    if (collision.transform.GetChild(body).GetComponent<Target>() != null)
                    {
                        collision.transform.GetChild(body).GetComponent<Target>().enabled = false;
                    }
                }
                transform.GetComponent<ShipVariables>().UpdateShip();
            }
        }

        private IEnumerator OnTrigger(Collider2D collision)
        {
            LocationText.GetComponent<Text>().text = locName;
            for (float a = 0; a < 1; a += .05f)
            {
                LocationText.GetComponent<Text>().color = new Color(156, 152, 116, a);
                yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(3);

            for (float i = 1; i > -.1; i -= .05f)
            {
                LocationText.GetComponent<Text>().color = new Color(156, 152, 116, i);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}