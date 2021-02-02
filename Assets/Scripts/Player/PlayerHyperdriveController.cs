using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHyperdriveController : MonoBehaviour
    {
    
        private GameObject player;
        private GameObject ship;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    
        public void PlayerAutoPilot(Transform target)
        {
            if (player.transform.childCount > 0)
            {
                ship = player.transform.GetChild(0).gameObject;
                if (ship.GetComponent<ShipVariables>().HasHyperDrive == true)
                {
                    GameObject.Find("/SceneScripts").GetComponent<HyperDriveController>().AutoPilot(target, ship);
                }
            }
        }
    }
}
