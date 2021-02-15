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
        private bool canControl;

        void Start()
        {
            sceneScripts = GameObject.Find("/SceneScripts");
            ship = this.transform.GetChild(0).gameObject;
            keyMap = sceneScripts.GetComponent<KeyMap>();
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            stearing = sceneScripts.GetComponent<SteeringController>();
            canControl = ship.GetComponent<ShipVariables>().CanControl;
            setupShip.CreateWeapon(ship);
            setupShip.CreateWeapon(ship);
        }

        void FixedUpdate()
        {
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
                    sceneScripts.GetComponent<ThrusterController>().Accelerate(ship);
                }

                if (Input.GetKeyUp(keyMap.Foreward))
                {
                    thrusterA.transform.GetChild(0).gameObject.SetActive(false);
                    thrusterB.transform.GetChild(0).gameObject.SetActive(false);
                }

                if (Input.GetKey(keyMap.Fire))
                {
                    Transform weaponSlots = ship.transform.GetChild(0).GetChild(0);
                    foreach (Transform weapon in weaponSlots)
                    {
                        if (weapon.childCount > 0)
                        {
                            sceneScripts.GetComponent<WeaponController>().Shoot(ship, weapon.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }
}
