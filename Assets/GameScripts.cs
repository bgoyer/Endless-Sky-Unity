using Assets.Scripts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScripts : MonoBehaviour
{
    public static GameScripts Instance;

    private float rotSpeed;
    private Rigidbody2D r2D;
    private GameObject thrusterA;
    private GameObject thrusterB;
    private bool takeEnergy = true;
    private bool gainHeat = true;
    private WaitForSeconds delay = new WaitForSeconds(.01f);
    private ObjectPooler objPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objPool = ObjectPooler.Instance;
    }

    #region Steering

    //private bool takeEnergy = true;
    //private bool gainHeat = true;

    public void TurnLeft(GameObject ship, bool overide = false)
    {
        if (ship.GetComponent<ShipVariables>().CanControl == false && overide == false) return;
        rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
        r2D = ship.GetComponent<Rigidbody2D>();
        r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
        //if (takeEnergy)
        //{
        //    takeEnergy = false;
        //    ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption;
        //}
        //if (gainHeat)
        //{
        //    gainHeat = false;
        //    ship.GetComponent<ShipVariables>().Temp += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration;
        //}
    }

    public void TurnRight(GameObject ship, bool overide = false)
    {
        if (ship.GetComponent<ShipVariables>().CanControl == false && overide == false) return;
        rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
        r2D = ship.GetComponent<Rigidbody2D>();
        r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
        //if (takeEnergy)
        //{
        //    takeEnergy = false;
        //    ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption;
        //}
        //if (gainHeat)
        //{
        //    gainHeat = false;
        //    ship.GetComponent<ShipVariables>().Temp += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration;
        //}
    }

    public void RotateTowards(GameObject ship, Vector3 direction, float multiplier = 1, bool overide = false)
    {
        if (ship.GetComponent<ShipVariables>().CanControl == false && overide == false) return;
        rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
        r2D = ship.GetComponent<Rigidbody2D>();
        Vector3 forwardVector = ship.transform.up;
        float angle = Vector3.Angle(forwardVector, direction);
        if (Vector3.Cross(forwardVector, direction).z < 0)
        {
            ship.transform.Rotate(0, 0, (angle * -1) * rotSpeed * multiplier / 100);
        }
        else
        {
            ship.transform.Rotate(0, 0, (angle) * rotSpeed * multiplier / 100);
        }
        //if (takeEnergy)
        //{
        //    takeEnergy = false;
        //    ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption;
        //}
        //if (gainHeat)
        //{
        //    gainHeat = false;
        //    ship.GetComponent<ShipVariables>().Temp += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration;
        //}
    }

    public void StopStearing(GameObject ship)
    {
        ship.GetComponent<ShipVariables>().CurrentBatteryEnergy += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption;
        ship.GetComponent<ShipVariables>().Temp -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration;
        //if (!takeEnergy || !gainHeat)
        //{
        //    takeEnergy = true;
        //    gainHeat = true;
        //}
    }

    #endregion Steering

    #region Thruster

    public void Accelerate(GameObject ship, bool overide = false)
    {
        if (ship.transform.GetComponent<ShipVariables>().CanControl == true || overide == true)
        {
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(true);
            thrusterB.transform.GetChild(0).gameObject.SetActive(true);
            r2D = ship.GetComponent<Rigidbody2D>();
            if (ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= thrusterA.transform.GetComponent<ThrusterVariables>().MaxSpeed)
            {
                r2D.AddRelativeForce(Vector3.up * (float)thrusterA.transform.GetComponent<ThrusterVariables>().ThrustEnergy / 2);
            }
            else
            {
                ship.transform.GetComponent<Rigidbody2D>().velocity *= .999f;
            }
            //if (takeEnergy == true)
            //{
            //    takeEnergy = false;
            //    ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= thrusterA.GetComponent<ThrusterVariables>().EnergyConsumption;
            //}
            //if (gainHeat == true)
            //{
            //    gainHeat = false;
            //    ship.GetComponent<ShipVariables>().Temp += thrusterA.GetComponent<ThrusterVariables>().HeatGeneration;
            //}
        }
    }

    public void StopThruster(GameObject ship)
    {
        if (takeEnergy == false || gainHeat == false)
        {
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(false);
            thrusterB.transform.GetChild(0).gameObject.SetActive(false);
            ship.GetComponent<ShipVariables>().Temp -= thrusterA.GetComponent<ThrusterVariables>().HeatGeneration;
            ship.GetComponent<ShipVariables>().CurrentBatteryEnergy += thrusterA.GetComponent<ThrusterVariables>().EnergyConsumption;
            takeEnergy = true;
            gainHeat = true;
        }
    }

    #endregion Thruster

    public class KeyMap : MonoBehaviour
    {
        public KeyCode Foreward = KeyCode.W;
        public KeyCode TurnLeft = KeyCode.A;
        public KeyCode TurnAround = KeyCode.S;
        public KeyCode TurnRight = KeyCode.D;
        public KeyCode OpenMap = KeyCode.M;
        public KeyCode PauseMenu = KeyCode.Escape;
        public KeyCode Land = KeyCode.E;
        public KeyCode Fire = KeyCode.Space;
        public KeyCode CancelWarp = KeyCode.C;
    }
}