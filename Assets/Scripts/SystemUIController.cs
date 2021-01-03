using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SystemUIController : MonoBehaviour
{
    public GameObject LocationText;

    private void OnTriggerEnter2D()
    {
        StartCoroutine("OnTrigger");
    }

    private IEnumerator OnTrigger()
    {
        LocationText.GetComponent<Text>().text = this.transform.gameObject.name.ToUpper();

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

    private void OnTriggerExit2D()
    {
        for (int body = 0; body < this.transform.childCount; body++)
        {
            if (this.transform.GetChild(body).GetComponent<Target>() != null)
            {
                this.transform.GetChild(body).GetComponent<Target>().enabled = false;
            }
        }
    }
}