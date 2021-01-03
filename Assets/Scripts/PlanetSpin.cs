using System.Collections;
using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    public float rotate_time = 5.0f;
    private Renderer Rdr;

    private void Start()
    {
        Rdr = GetComponent<Renderer>();
    }

    private void Update()
    {
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        if (Rdr.isVisible)
        {
            transform.Rotate(0, 0, (360 / ((rotate_time) * 60 * 60 * 10)) * Time.deltaTime, Space.Self);
            yield return new WaitForSeconds(.01f);
        }
    }
}