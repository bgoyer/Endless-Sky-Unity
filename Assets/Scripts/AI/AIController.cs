using System.Collections;
using Assets.Resources.Data.ModelData;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AIController : MonoBehaviour
    {

        public GameObject target;
        private SetUpAndSwitchShip setupShip;
        private GameObject sceneScripts;
        private GameObject ship;
        private static GameObject Systems;
        private string currentSystem;

        void Start()
        {
            Systems = GameObject.Find("/Systems");
            sceneScripts = GameObject.Find("/SceneScripts");
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            ship = this.transform.GetChild(0).gameObject;
            setupShip.CreateWeapon(ship);
            setupShip.CreateWeapon(ship);
        }
        public void Hit(Collider2D col)
        {
            if (col.transform.GetComponent<BulletController>())
            {
                if (col.transform.GetComponent<BulletController>().parentShip != ship)
                {
                    this.GetComponent<Animator>().SetInteger("State",3);
                }
            }
        }
        public IEnumerator Warp()
        {
            currentSystem = ship.transform.parent.parent.parent.name;
            GameObject chosenSystem = chooseSystem();
            Debug.Log(chosenSystem.name);
            while (chosenSystem.name == currentSystem)
            {
                chosenSystem = chooseSystem();
                Debug.Log(chosenSystem.name);
                yield return new WaitForSeconds(.1f);
            }


            sceneScripts.GetComponent<HyperDriveController>().AutoPilot(chosenSystem.transform, ship);
            yield return new WaitUntil(() => ship.GetComponent<Rigidbody2D>().velocity.magnitude > 20);
            yield return new WaitUntil(() => ship.GetComponent<Rigidbody2D>().velocity.magnitude == 0);
            transform.parent.GetComponent<Animator>().SetInteger("State", 0);
            currentSystem = ship.transform.parent.parent.parent.name;
        }

        private GameObject chooseSystem()
        {
            int cluster = new int();
            int system = new int();
            cluster = Random.Range(1, Systems.transform.childCount);
            system = Random.Range(1, Systems.transform.GetChild(cluster).childCount);
            return Systems.transform.GetChild(cluster).GetChild(system).gameObject;
        }
    }
}
