using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("FPScounter");
    }

    private IEnumerator FPScounter()
    {
        while (true)
        {
            float fps = 1.0f / Time.deltaTime;
            this.transform.GetComponent<Text>().text = fps.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}