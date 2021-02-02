using System.Collections;
using Assets.Scripts.OffScreenIndicator;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SystemUIController : MonoBehaviour
    {
        public GameObject LocationText;
        public GameObject systemUI;

        private string _locName;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("System"))
            {
                _locName = collision.gameObject.name.ToUpper();
                StartCoroutine(OnTrigger(collision));
                systemUI.GetComponent<Text>().text = collision.transform.name;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("System"))
            {
                _locName = collision.transform.parent.gameObject.name.ToUpper();
                StartCoroutine(OnTrigger(collision));
                systemUI.GetComponent<Text>().text = collision.transform.parent.gameObject.name;
                for (int body = 0; body < collision.transform.childCount; body++)
                {
                    if (collision.transform.GetChild(body).GetComponent<Target>() != null)
                    {
                        collision.transform.GetChild(body).GetComponent<Target>().enabled = false;
                    }
                }
            }

        }

        private IEnumerator OnTrigger(Collider2D collision)
        {
            LocationText.GetComponent<Text>().text = _locName;

            for (int body = 0; body < collision.transform.childCount; body++)
            {
                if (collision.transform.GetChild(body).GetComponent<Target>() != null)
                {
                    collision.transform.GetChild(body).GetComponent<Target>().enabled = true;
                }
            }
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