using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class SteeringController : MonoBehaviour
    {
        private float rotSpeed;
        private Rigidbody2D r2D;

        private bool takeEnergy = true;
        private bool gainHeat = true;
        private WaitForSeconds delay = new WaitForSeconds(.01f);
        public void TurnLeft(GameObject ship, bool overide = false)
        {
            if (!ship.GetComponent<ShipVariables>().CanControl && !overide) return;
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
            if (takeEnergy)
            {
                StartCoroutine("TakeEnergy",ship);
            }
            if (gainHeat)
            {
                StartCoroutine("GainHeat", ship);
            }
        }

        public void TurnRight(GameObject ship, bool overide = false)
        {
            if (!ship.GetComponent<ShipVariables>().CanControl && !overide) return;
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
            if (takeEnergy)
            {
                StartCoroutine("TakeEnergy", ship);
            }
            if (gainHeat)
            {
                StartCoroutine("GainHeat", ship);
            }
        }

        public void RotateTowards(GameObject ship, Vector3 direction, float multiplier = 1, bool overide = false)
        {
            if (!ship.GetComponent<ShipVariables>().CanControl && !overide) return;
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
            if (takeEnergy)
            {
                StartCoroutine("TakeEnergy", ship);
            }
            if (gainHeat)
            {
                StartCoroutine("GainHeat", ship);
            }
        }

        private IEnumerator TakeEnergy(GameObject ship)
        {
            takeEnergy = false;
            if ((ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption) > 0)
            {
                ship.GetComponent<ShipVariables>().CurrentBatteryEnergy -= ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().EnergyConsumption;

            }else { ship.GetComponent<ShipVariables>().CurrentBatteryEnergy = 0; }
            yield return delay;
            takeEnergy = true;
        }
        private IEnumerator GainHeat(GameObject ship)
        {
            gainHeat = false;
            if ((ship.GetComponent<ShipVariables>().Temp += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration) < ship.GetComponent<ShipVariables>().OverHeatTemp)
            {
                ship.GetComponent<ShipVariables>().Temp += ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().HeatGeneration;
            }
            else { ship.GetComponent<ShipVariables>().Temp = ship.GetComponent<ShipVariables>().OverHeatTemp; }
            yield return delay;
            gainHeat = true;
        }
    }
}