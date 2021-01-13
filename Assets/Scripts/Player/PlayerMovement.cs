using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 1f;

    public float rotSpeed = 1f;

    public float WarpRotSpeed = 10f;
    public float WarpSpeed = 50f;
    public GameObject PlayerShip;
    public bool ignoreY;
    public Rigidbody2D R2D;
    private Vector2 CurrentVelocity;
    private static GameObject AutoPilotTarget;
    public bool canControl = true;
    private PlayerAudioControler Audio;

    private void Start()
    {
        Audio = this.transform.GetComponent<PlayerAudioControler>();
    }

    private void FixedUpdate()
    {
        CurrentVelocity = R2D.velocity;
        if (canControl == true)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShip.transform.Rotate(0f, 0f, rotSpeed, Space.Self);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (canControl == true)
                {
                    if (PlayerShip.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 50f)
                    {
                        R2D.AddRelativeForce(Vector3.up * acceleration);
                    }
                    else
                    {
                        PlayerShip.transform.GetComponent<Rigidbody2D>().velocity *= 0.999f;
                    }
                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShip.transform.Rotate(0f, 0f, -rotSpeed, Space.Self);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                StartCoroutine("RotateNegVel");
            }
        }
    }

    public void SetControl(bool state)
    {
        canControl = state;
    }

    private bool active = false;

    private IEnumerator RotateNegVel()
    {
        if (active == false)
        {
            active = true;
            float angle = (Mathf.Atan2(-PlayerShip.GetComponent<Rigidbody2D>().velocity.y, -PlayerShip.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg) - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            for (float i = 0; i <= 1; i += (rotSpeed / 5000))
            {
                PlayerShip.transform.rotation = Quaternion.Slerp(PlayerShip.transform.rotation, q, i);
                yield return new WaitForSeconds(.01f);
                if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
                {
                    active = false;
                    break;
                }
            }
            active = false;
        }
    }
}