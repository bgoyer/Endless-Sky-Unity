using System.Collections;
using Assets.Resources.Data.ModelData;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AIController : MonoBehaviour
    {
        private bool doingSomething = false;
        private static GameObject SceneScripts;
        private static GameObject Systems;
        private string currentSystem;
        private SetUpAndSwitchShip setupShip;
        private GameObject target;
        private GameObject sceneScripts;

        void Start()
        {
            target = GameObject.Find("/Player");
            SceneScripts = GameObject.Find("/SceneScripts");
            Systems = GameObject.Find("/Systems");
            currentSystem = "";
            sceneScripts = GameObject.Find("/SceneScripts");
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            setupShip.CreateWeapon(this.transform.GetChild(0).gameObject);
            setupShip.CreateWeapon(this.transform.GetChild(0).gameObject);
        }
        void Update()
        {
            followTarget();
        }

        private void followTarget()
        {
            GameObject ship = this.transform.GetChild(0).gameObject;
            if (Vector3.Distance(ship.transform.position , target.transform.GetChild(0).position) < 5 && this.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude > 5)
            {
                SceneScripts.GetComponent<SteeringController>().RotateTowards(this.transform.GetChild(0).gameObject, (this.transform.GetChild(0).position - target.transform.GetChild(0).position).normalized);
                SceneScripts.GetComponent<ThrusterController>().Accelerate(this.transform.GetChild(0).gameObject);
            }
            else
            {
                SceneScripts.GetComponent<ThrusterController>().Accelerate(this.transform.GetChild(0).gameObject);
                SceneScripts.GetComponent<SteeringController>().RotateTowards(this.transform.GetChild(0).gameObject, -(this.transform.GetChild(0).position - target.transform.GetChild(0).position).normalized);
            }
            var weaponSlots = ship.transform.GetChild(0).GetChild(0);
            foreach (Transform weapon in weaponSlots)
            {
                if (weapon.childCount > 0)
                {
                    sceneScripts.GetComponent<WeaponController>().Shoot(ship, weapon.transform.GetChild(0).gameObject);
                }
            }
        }
        private IEnumerator warp()
        {
            GameObject chosenSystem = chooseSystem();
            currentSystem = chosenSystem.name;
            if (chosenSystem.name != currentSystem)
            {
                print(chosenSystem.name +", "+ currentSystem);
                SceneScripts.GetComponent<HyperDriveController>().AutoPilot(chosenSystem.transform, this.transform.GetChild(0).gameObject);
                yield return new WaitUntil(() => this.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude > 20);
                yield return new WaitUntil(() => this.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude == 0);
                doingSomething = false;
            }
            else
            {
            
            }
        }

        private GameObject chooseSystem()
        {
            int cluster = UnityEngine.Random.Range(0, Systems.transform.childCount);
            int system = UnityEngine.Random.Range(0, Systems.transform.GetChild(cluster).childCount);
            return Systems.transform.GetChild(cluster).GetChild(system).gameObject;
        }
    }
}
