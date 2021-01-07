using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SystemUIController : MonoBehaviour
{
    public GameObject LocationText;

    private string _locName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            _locName = this.transform.gameObject.name.ToUpper();
            StartCoroutine("OnTrigger");
            GameObject.Find("/HUD/HideWhenMapIsOpen/PlayerStats/System").GetComponent<Text>().text = this.transform.gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            _locName = this.transform.parent.gameObject.name.ToUpper();
            StartCoroutine("OnTrigger");
            GameObject.Find("/HUD/HideWhenMapIsOpen/PlayerStats/System").GetComponent<Text>().text = this.transform.parent.gameObject.name;
            for (int body = 0; body < this.transform.childCount; body++)
            {
                if (this.transform.GetChild(body).GetComponent<Target>() != null)
                {
                    this.transform.GetChild(body).GetComponent<Target>().enabled = false;
                }
            }
        }

    }

    private IEnumerator OnTrigger()
    {
        LocationText.GetComponent<Text>().text = _locName;

        for (int body = 0; body < this.transform.childCount; body++)
        {
            if (this.transform.GetChild(body).GetComponent<Target>() != null)
            {
                this.transform.GetChild(body).GetComponent<Target>().enabled = true;
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