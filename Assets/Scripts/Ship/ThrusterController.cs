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

        public void Accelerate(GameObject ship)
        {
            if (ship.transform.GetComponent<ShipVariables>().CanControl == true)
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
                    StartCoroutine("TakeEnergy", ship);
                }
                if (gainHeat)
                {
                    StartCoroutine("GainHeat", ship);
                }
            }
        }

        public void StopThruster(GameObject ship)
        {
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(false);
            thrusterB.transform.GetChild(0).gameObject.SetActive(false);
        }
        private IEnumerator TakeEnergy(GameObject ship)
        {
            takeEnergy = false;
            if ((ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= thrusterA.transform.GetComponent<ThrusterVariables>().EnergyConsumption) > 0)
            {
                ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= thrusterA.transform.GetComponent<ThrusterVariables>().EnergyConsumption;

            }
            else { ship.GetComponent<ShipVariables>().CurrentBatteryEnergy = 0; }
            yield return delay;
            takeEnergy = true;
        }
        private IEnumerator GainHeat(GameObject ship)
        {
            gainHeat = false;
            if ((ship.GetComponent<ShipVariables>().Temp += thrusterA.transform.GetComponent<ThrusterVariables>().HeatGeneration) < ship.GetComponent<ShipVariables>().OverHeatTemp)
            {
                ship.GetComponent<ShipVariables>().Temp += thrusterA.transform.GetComponent<ThrusterVariables>().HeatGeneration;
            }
            else { ship.GetComponent<ShipVariables>().Temp = ship.GetComponent<ShipVariables>().OverHeatTemp; }
            yield return delay;
            gainHeat = true;
        }
    }
}