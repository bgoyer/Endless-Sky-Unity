using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAreaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        transform.GetChild(0).position = new Vector2(Random.Range(0, 10), Random.Range(-4, 4));
    }
}
