using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StearingController : MonoBehaviour
{
    public float rotSpeed = 1f;
    public float WarpRotSpeed = 10f;

    private KeyMap keyMap;
    private GameObject ship;
    private bool canControl;
    private Rigidbody2D r2D;
    private bool active = false;


    private void Start()
    {
        keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
        ship = this.transform.parent.parent.gameObject;
        canControl = ship.gameObject.GetComponent<ShipVariables>().CanControl;
        r2D = ship.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (canControl == true)
        {
            if (Input.GetKey(keyMap.TurnLeft))
            {
                TurnLeft();
            }

            if (Input.GetKey(keyMap.TurnRight))
            {
                TurnRight();
            }

            if (Input.GetKey(keyMap.TurnAround))
            {
                StartCoroutine("RotateNegVel");
            }
        }
    }

    public void TurnLeft()
    {
        r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
    }
    public void TurnRight()
    {
        r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
    }

    public IEnumerator RotateNegVel()
    {
        if (active == false)
        {
            var lastVelVector = r2D.velocity;
            active = true;
            float angle = (Mathf.Atan2(-ship.GetComponent<Rigidbody2D>().velocity.y, -ship.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg) - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            for (float i = 0; i <= 1; i += (rotSpeed / 3000))
            {
                if (lastVelVector != r2D.velocity)
                {
                    angle = (Mathf.Atan2(-ship.GetComponent<Rigidbody2D>().velocity.y, -ship.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg) - 90;
                    q = Quaternion.AngleAxis(angle, Vector3.forward);
                    i = 0;

                }
                ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, q, i);
                yield return new WaitForSeconds(.01f);
                if (Input.GetKeyUp(keyMap.TurnAround))
                {
                    break;
                }
            }
            active = false;
        }
    }
}
