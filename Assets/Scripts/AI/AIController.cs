using System.Collections;
using Assets.Resources.Data.ModelData;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AIController : MonoBehaviour
    {

        public GameObject Target;
        private SetUpAndSwitchShip setupShip;
        private GameObject sceneScripts;
        private GameObject ship;
        private static GameObject _systems;
        private string currentSystem;

        void Start()
        {
            _systems = GameObject.Find("/Systems");
            sceneScripts = GameObject.Find("/SceneScripts");
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            ship = this.transform.GetChild(0).gameObject;
            setupShip.CreateWeapon(ship, "Energy Blaster");
            setupShip.CreateWeapon(ship, "Energy Blaster");
                setupShip.CreateEngine(ship, "Greyhound Plasma Thruster");
        }
        public void Hit(Collider2D col)
        {
            if (col.transform.GetComponent<BulletController>())
            {
                if (col.transform.GetComponent<BulletController>().ParentShip != ship && ship.GetComponent<ShipVariables>().HullHp > 0)
                {
                    Target = col.transform.GetComponent<BulletController>().ParentShip;
                    this.GetComponent<Animator>().SetInteger("State",3);
                }
            }
        }
    }
}
