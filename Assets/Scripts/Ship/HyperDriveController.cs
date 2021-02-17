using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class HyperDriveController : MonoBehaviour
    {
        private int warpThrust;
        private float warpRotSpeed;
        private AudioSource warpingSfx;
        private AudioSource inWarpSfx;
        private bool canControl;
        private Rigidbody2D r2D;
        private Vector2 currentVelocity;
        private ShipVariables shipVar;

        private SteeringController steering;
        //private int currentFuel;
        private void Start()
        {
            steering = GameObject.Find("/SceneScripts").GetComponent<SteeringController>();
        }
        public void AutoPilot(Transform target, GameObject ship)
        {
            //currentFuel = shipVar.HyperdriveFuel;
            StartCoroutine(StartAutoPilot(target, ship));
        }

        private IEnumerator StartAutoPilot(Transform target, GameObject ship)
        {
            shipVar = ship.GetComponent<ShipVariables>();
            canControl = shipVar.CanControl;
            r2D = ship.GetComponent<Rigidbody2D>();
            Debug.Log(ship.transform.GetChild(3).name);
            WarpdriveVariables warpVar = ship.transform.GetChild(3).GetChild(0).GetComponent<WarpdriveVariables>();
            warpThrust = warpVar.WarpThrust;
            warpRotSpeed = warpVar.RotSpeed;
            warpingSfx = ship.transform.GetChild(3).GetChild(0).GetChild(1).gameObject.GetComponent<AudioSource>();
            inWarpSfx = ship.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<AudioSource>();
            float distance = Vector3.Distance(target.position, ship.transform.position);
            if (distance >= 50 && canControl == true)
            {

                canControl = false;

                r2D.drag = 10f;

                yield return new WaitUntil(() => r2D.velocity.magnitude == 0);

                r2D.drag = 0f;

                Vector3 vectorToTarget = -(ship.transform.position - target.position).normalized;
                while (Vector3.Angle(ship.transform.up, vectorToTarget) > 0)
                {
                    steering.RotateTowards(ship, vectorToTarget, 3);
                    yield return new WaitForSeconds(0.01f);

                }

                SoundWarp();

                yield return new WaitForSeconds(1.6f);

                SoundInWarp();

                r2D.AddRelativeForce(Vector2.up * warpThrust, ForceMode2D.Impulse);

                while (distance > 200)
                {
                    distance = Vector3.Distance(target.position, ship.transform.position);
                    yield return null;
                }
                r2D.drag = 2.5f;

                StopSound();

                yield return new WaitUntil(() => r2D.velocity.magnitude == 0);

                canControl = true;

                r2D.drag = 0f;
            }
        }
        private void SoundWarp()
        {
            warpingSfx.volume = 1;
            warpingSfx.Play();
        }

        private void SoundInWarp()
        {
            inWarpSfx.volume = 1;
            inWarpSfx.Play();
        }

        private void StopSound()
        {
            StartCoroutine("StopSounds");
        }

        private IEnumerator StopSounds()
        {
            for (float i = 1; i > -.1; i -= .1f)
            {
                warpingSfx.volume = i;
                inWarpSfx.volume = i;
                yield return new WaitForSeconds(.1f);
            }
        }


    }
}
