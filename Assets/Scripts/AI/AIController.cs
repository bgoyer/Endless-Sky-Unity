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
        private AI_Variables aIStats;

        private void Start()
        {
            _systems = GameObject.Find("/Systems");
            sceneScripts = GameObject.Find("/SceneScripts");
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            ship = this.transform.GetChild(0).gameObject;
            aIStats = ship.transform.parent.GetComponent<AI_Variables>();
        }

        public void Attacked(Collider2D col)
        {
            Debug.Log("HIT");
            if (aIStats.Class == "Pirate" || aIStats.Class == "Police")
            {
                if (col.transform.GetComponent<BulletController>())
                {
                    if (col.transform.GetComponent<BulletController>().ParentShip != ship && ship.GetComponent<ShipVariables>().HullHp > 0)
                    {
                        Target = col.transform.GetComponent<BulletController>().ParentShip;
                        this.GetComponent<Animator>().SetInteger("State", 3);
                    }
                }
            }
        }
    }
}