using System.Collections;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class PlanetSpin : MonoBehaviour
    {
        public float RotateTime = 5.0f;
        private Renderer rdr;

        private void Start()
        {
            rdr = GetComponent<Renderer>();
        }

        private void Update()
        {
            StartCoroutine("Rotate");
        }

        private IEnumerator Rotate()
        {
            if (rdr.isVisible)
            {
                transform.Rotate(0, 0, (RotateTime) * Time.deltaTime, Space.Self);
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}