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
        private StearingController stearing;
        private bool canControl;

        void Start()
        {
            sceneScripts = GameObject.Find("/SceneScripts");
            ship = this.transform.GetChild(0).gameObject;
            keyMap = sceneScripts.GetComponent<KeyMap>();
            setupShip = sceneScripts.GetComponent<SetUpAndSwitchShip>();
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            canControl = ship.GetComponent<ShipVariables>().CanControl;
            stearing = ship.transform.GetChild(2).GetChild(0).GetComponent<StearingController>();
            setupShip.CreateWeapon(ship);
            setupShip.CreateWeapon(ship);
        }

        void Update()
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

                if (Input.GetKeyDown(keyMap.TurnAround))
                {
                    stearing.StartCoroutine("RotateNegVel", ship);
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
                            weapon.GetChild(0).GetComponent<WeaponController>()
                                .Shoot(ship.GetComponent<Rigidbody2D>().velocity.magnitude);
                        }
                    }
                }
            }
        }
    }
}
