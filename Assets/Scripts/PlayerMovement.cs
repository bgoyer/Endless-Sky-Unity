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

    public void AutoPilot(Transform target)
    {
        StartCoroutine(StartAutoPilot(target));
    }

    private IEnumerator StartAutoPilot(Transform target)
    {
        canControl = true;
        float distance = Vector3.Distance(target.position, PlayerShip.transform.position);
        if (distance >= 500)
        {
            canControl = false;

            R2D.drag = 10f;

            yield return new WaitUntil(() => CurrentVelocity.x == 0 && CurrentVelocity.y == 0);

            R2D.drag = 0f;

            Vector3 vectorToTarget = target.position - PlayerShip.transform.position;

            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;

            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            for (float i = 0; i <= 1; i += (WarpRotSpeed / 6000))
            {
                PlayerShip.transform.rotation = Quaternion.Slerp(PlayerShip.transform.rotation, q, i);
                yield return new WaitForSeconds(.01f);
            }

            Audio.Warp();

            yield return new WaitForSeconds(1.6f);

            Audio.InWarp();

            R2D.AddRelativeForce(Vector2.up * WarpSpeed * 10, ForceMode2D.Impulse);

            while (distance > 200)
            {
                distance = Vector3.Distance(target.position, PlayerShip.transform.position);
                yield return null;
            }
            R2D.drag = 2.5f;

            Audio.StopSound();

            yield return new WaitUntil(() => CurrentVelocity.x == 0 && CurrentVelocity.y == 0);

            canControl = true;

            R2D.drag = 0f;
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