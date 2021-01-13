using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    public float Acceleration = 10f;
    public float MaxSpeed = 10f;

    private KeyMap keyMap;
    private GameObject ship;
    private bool canControl;
    private Rigidbody2D r2D;
    

    private void Start()
    {
        keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
        ship = this.transform.parent.parent.parent.gameObject;
        canControl = ship.gameObject.GetComponent<ShipVariables>().CanControl;
        r2D = ship.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (Input.GetKey(keyMap.Foreward))
        {
            Accelerate();
        }
        if (Input.GetKeyUp(keyMap.Foreward))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Accelerate()
    {
        if (canControl == true)
        {
            if (ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= MaxSpeed)
            {
                r2D.AddRelativeForce(Vector3.up * Acceleration);
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                ship.transform.GetComponent<Rigidbody2D>().velocity *= 0.999f;
            }
        }
    }
}
