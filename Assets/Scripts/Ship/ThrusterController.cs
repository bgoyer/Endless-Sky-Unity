using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ThrusterController : MonoBehaviour
    {
        private GameObject thrusterA;
        private GameObject thrusterB;
        private Rigidbody2D r2D;
        private bool takeEnergy = true;
        private bool gainHeat = true;
        private WaitForSeconds delay = new WaitForSeconds(.01f);

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
                if (takeEnergy)
                {
                    takeEnergy = false;
                    ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= thrusterA.GetComponent<ThrusterVariables>().EnergyConsumption;
                }
                if (gainHeat)
                {
                    gainHeat = false;
                    ship.GetComponent<ShipVariables>().Temp += thrusterA.GetComponent<ThrusterVariables>().HeatGeneration;
                }
            }
        }

        public void StopThruster(GameObject ship)
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
}