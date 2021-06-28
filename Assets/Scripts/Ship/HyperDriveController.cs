using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class HyperDriveController : MonoBehaviour
    {
        public GameObject PlayerWarning;

        private float warpRotSpeed;
        private AudioSource warpingSfx;
        private AudioSource inWarpSfx;
        private Rigidbody2D r2D;
        private Vector2 currentVelocity;
        private ShipVariables shipVar;
        private TrailRenderer trail;
        private bool cancelWarp = false;
        private SteeringController steering;
        private ThrusterController thruster;

        private void Start()
        {
            steering = GameObject.Find("/SceneScripts").GetComponent<SteeringController>();
            thruster = GameObject.Find("/SceneScripts").GetComponent<ThrusterController>();
        }

        public void AutoPilot(Transform target, GameObject ship)
        {
            StartCoroutine(StartAutoPilot(target, ship));
        }

        private IEnumerator StartAutoPilot(Transform target, GameObject ship)
        {
            HyperdriveVariables warpVar = ship.transform.GetChild(3).GetChild(0).GetComponent<HyperdriveVariables>();
            float timeStart = Time.realtimeSinceStartup;
            shipVar = ship.GetComponent<ShipVariables>();
            r2D = ship.GetComponent<Rigidbody2D>();
            warpingSfx = ship.transform.GetChild(3).GetChild(0).GetChild(1).gameObject.GetComponent<AudioSource>();
            inWarpSfx = ship.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<AudioSource>();
            trail = ship.transform.GetChild(3).GetChild(0).gameObject.GetComponent<TrailRenderer>();
            float distance = Vector3.Distance(target.position, ship.transform.position);
            
            if (distance >= 50 && ship.GetComponent<ShipVariables>().HyperdriveFuel >= Mathf.CeilToInt((float)distance * (float)warpVar.FuelUsage))
            {
                float startDistance = Vector3.Distance(target.position, ship.transform.position);
                ship.GetComponent<ShipVariables>().CanControl = false;
                while (Vector3.Angle(ship.transform.up, -r2D.velocity) > 0)
                {
                    steering.RotateTowards(ship, -r2D.velocity, 3, true);
                    yield return new WaitForSeconds(0.01f);
                    steering.StopStearing(ship);
                }
                
                while (r2D.velocity.magnitude > .2f)
                {
                    thruster.Accelerate(ship, true);
                    yield return new WaitForSeconds(0.01f);
                    Debug.Log(r2D.velocity.magnitude);
                }
                thruster.StopThruster(ship);
                
                Vector3 vectorToTarget = -(ship.transform.position - target.position).normalized;
                while (Vector3.Angle(ship.transform.up, vectorToTarget) > 0)
                {
                    steering.RotateTowards(ship, vectorToTarget, 3, true);
                    yield return new WaitForSeconds(0.01f);
                    steering.StopStearing(ship);
                }
                
                SoundWarp();

                yield return new WaitForSeconds(1.6f);

                SoundInWarp();
                trail.emitting = true;
                r2D.AddRelativeForce(Vector2.up * warpVar.WarpThrust, ForceMode2D.Impulse);
                int startFuel = shipVar.HyperdriveFuel;
                int endFuel = Mathf.CeilToInt((float)distance * (float)warpVar.FuelUsage);
                while (distance > 200)
                {
                    ship.GetComponent<ShipVariables>().CanControl = false;
                    shipVar.HyperdriveFuel = (int)Mathf.Lerp(endFuel, startFuel, (distance / startDistance));
                    yield return new WaitForSeconds(.01f);
                    distance = Vector3.Distance(target.position, ship.transform.position);
                    if (ship.CompareTag("PlayerShip"))
                    {
                        if (Input.GetKey(GameObject.Find("/SceneScripts").GetComponent<KeyMap>().CancelWarp))
                        {
                            CancelWarp();
                        }
                    }
                    if (cancelWarp == true)
                    {
                        cancelWarp = false;
                        r2D.drag = 2.5f;

                        StopSound();

                        yield return new WaitUntil(() => r2D.velocity.magnitude == 0);
                        trail.emitting = false;
                        r2D.drag = 0f;

                        ship.GetComponent<ShipVariables>().CanControl = true;
                        yield break;
                    }
                    yield return null;
                }
                r2D.drag = 2.5f;

                StopSound();
                yield return new WaitUntil(() => r2D.velocity.magnitude == 0);
                trail.emitting = false;
                r2D.drag = 0f;
                ship.GetComponent<ShipVariables>().CanControl = true;
            }else if (ship.GetComponent<ShipVariables>().HyperdriveFuel >= Mathf.CeilToInt((float)distance * .8f))
            {
                if (ship.CompareTag("PlayerShip"))
                {
                    PlayerWarning.SetActive(true);
                    yield return new WaitForSeconds(5);
                    PlayerWarning.SetActive(false);
                }
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
        public void CancelWarp()
        {
            cancelWarp = true;
        }
    }
}