using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    public float Acceleration = 10f;
    public float MaxSpeed = 10f;

    private KeyMap keyMap;
    private GameObject Ship;
    private bool canControl;
    private Rigidbody2D r2D;
    

    private void Start()
    {
        keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
        Ship = this.transform.parent.parent.parent.gameObject;
        canControl = Ship.transform.parent.gameObject.GetComponent<ShipVariables>().CanControl;
        r2D = Ship.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (Input.GetKey(keyMap.Foreward))
        {
            Accelerate();
        }
    }

    public void Accelerate()
    {
        if (canControl == true)
        {
            if (Ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= MaxSpeed)
            {
                r2D.AddRelativeForce(Vector3.up * Acceleration);
            }
            else
            {
                Ship.transform.GetComponent<Rigidbody2D>().velocity *= 0.999f;
            }
        }
    }
}
