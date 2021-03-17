using Assets.Resources.Data.ModelData;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShipControler : MonoBehaviour
    {
        private GameObject ship;
        private KeyMap keyMap;
        private SetUpAndSwitchShip setupShip;
        private GameObject sceneScripts;
        private GameObject thrusterA;
        private GameObject thrusterB;
        private GameObject weapon;
        private SteeringController stearing;
        private ThrusterController thruster;
        private WeaponController weapons;
        private bool canControl;

        private void Start()
        {
            this.gameObject.SetActive(false);
            sceneScripts = GameObject.Find("/SceneScripts");
            keyMap = sceneScripts.GetComponent<KeyMap>();
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            stearing = sceneScripts.GetComponent<SteeringController>();
            weapons = sceneScripts.GetComponent<WeaponController>();
            thruster = sceneScripts.GetComponent<ThrusterController>();
            ship = this.transform.GetChild(0).gameObject;
            canControl = ship.GetComponent<ShipVariables>().CanControl;
//            setupShip.CreateWeapon(ship, "Energy Blaster");
//            setupShip.CreateWeapon(ship, "Energy Blaster");
 //           setupShip.CreateEngine(ship, "Greyhound Plasma Thruster");
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            
            ship.GetComponent<ShipVariables>().UpdateShip();
            ship.GetComponent<ShipVariables>().CheckShip();
        }

        private void FixedUpdate()
        {
            if (!ship)
            {
                ship = this.transform.GetChild(0).gameObject;
            }
            if (canControl == true)
            {
                if (Input.GetKey(keyMap.TurnLeft))
                {
                    stearing.TurnLeft(ship);
                }

                if (Input.GetKey(keyMap.TurnRight))
                {
                    stearing.TurnRight(ship);
                }

                if (Input.GetKey(keyMap.TurnAround))
                {
                    stearing.RotateTowards(ship, -ship.GetComponent<Rigidbody2D>().velocity);
                }

                if (Input.GetKey(keyMap.Foreward))
                {
                    thruster.Accelerate(ship);
                }

                if (Input.GetKeyUp(keyMap.Foreward))
                {
                    thruster.StopThruster(ship);
                }

                if (Input.GetKey(keyMap.Fire))
                {
                    Transform weaponSlots = ship.transform.GetChild(0).GetChild(0);
                    foreach (Transform weapon in weaponSlots)
                    {
                        if (weapon.childCount > 0)
                        {
                            weapons.Shoot(ship, weapon.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }
}