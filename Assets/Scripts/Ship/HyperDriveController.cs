using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDriveController : MonoBehaviour
{
    public int WarpSpeed;
    public int WarpRotSpeed;

    private bool canControl;
    private GameObject ship;
    private Rigidbody2D r2D;
    private Vector2 currentVelocity;
    private AudioSource warpingSFX;
    private AudioSource inWarpSFX;
    private ShipVariables shipVar;
    private int currentFuel;
    private void Start()
    {
        shipVar = this.transform.parent.parent.GetComponent<ShipVariables>();
        canControl = shipVar.CanControl;
        currentFuel = shipVar.HyperdriveFuel;
        ship = this.transform.parent.parent.gameObject;
        r2D = ship.GetComponent<Rigidbody2D>();
        AudioSource warpingSFX = this.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        AudioSource inWarpSFX = this.transform.parent.GetChild(0).gameObject.GetComponent<AudioSource>();
    }
    public void AutoPilot(Transform target)
    {
        if (/**currentFuel >= Mathf.CeilToInt((ship.transform.position - target.position).magnitude)**/ true)
        {
            StartCoroutine(StartAutoPilot(target));
        }
        
    }

    private IEnumerator StartAutoPilot(Transform target)
    {
        canControl = true;
        float distance = Vector3.Distance(target.position, ship.transform.position);
        if (distance >= 500)
        {
            canControl = false;

            r2D.drag = 10f;

            yield return new WaitUntil(() => currentVelocity.x == 0 && currentVelocity.y == 0);

            r2D.drag = 0f;

            Vector3 vectorToTarget = target.position - ship.transform.position;

            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;

            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            for (float i = 0; i <= 1; i += (WarpRotSpeed / 6000))
            {
                ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, q, i);
                yield return new WaitForSeconds(.01f);
            }

            Warp();

            yield return new WaitForSeconds(1.6f);

            InWarp();

            r2D.AddRelativeForce(Vector2.up * WarpSpeed * 10, ForceMode2D.Impulse);

            while (distance > 200)
            {
                distance = Vector3.Distance(target.position, ship.transform.position);
                yield return null;
            }
            r2D.drag = 2.5f;

            stopSound();

            yield return new WaitUntil(() => currentVelocity.x == 0 && currentVelocity.y == 0);

            canControl = true;

            r2D.drag = 0f;
        }
    }
    private void Warp()
    {
        warpingSFX.volume = 1;
        warpingSFX.Play();
    }

    private void InWarp()
    {
        inWarpSFX.volume = 1;
        inWarpSFX.Play();
    }

    private void stopSound()
    {
        StartCoroutine("stopSounds");
    }

    private IEnumerator stopSounds()
    {
        for (float i = 1; i > -.1; i -= .1f)
        {
            warpingSFX.volume = i;
            inWarpSFX.volume = i;
            yield return new WaitForSeconds(.1f);
        }
    }


}
