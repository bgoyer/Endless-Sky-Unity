using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FPS : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            InvokeRepeating("FPSCounter", 0f, 1f);
        }

        private void FPSCounter()
        {
            float fps = 1.0f / Time.deltaTime;
            this.transform.GetComponent<Text>().text = fps.ToString();
        }
    }
}