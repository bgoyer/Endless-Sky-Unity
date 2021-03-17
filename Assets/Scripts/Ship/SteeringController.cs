using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class SteeringController : MonoBehaviour
    {
        private float rotSpeed;
        private Rigidbody2D r2D;

        public void TurnLeft(GameObject ship, bool overide = false)
        {
            if (!ship.GetComponent<ShipVariables>().CanControl && !overide) return;
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
        }

        public void TurnRight(GameObject ship, bool overide = false)
        {
            if (!ship.GetComponent<ShipVariables>().CanControl && !overide) return;
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
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
        }
    }
}