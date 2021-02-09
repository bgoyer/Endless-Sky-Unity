using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts.Ship
{
    public class SteeringController : MonoBehaviour
    {
        private float rotSpeed;
        private float lastAngle = 0;
        private Rigidbody2D r2D;

        public void TurnLeft(GameObject ship)
        {
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
        }

        public void TurnRight(GameObject ship)
        {
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
        }

        public void RotateTowards(GameObject ship, Vector3 direction)
        {
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<SteeringVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            Vector3 forwardVector = ship.transform.up;
            float angle = Vector3.Angle(forwardVector, direction);

            if (Vector3.Cross(forwardVector,direction).z < 0)
            {
                ship.transform.Rotate(0,0, (angle * -1) * rotSpeed / 100);
            }
            else
            {
                ship.transform.Rotate(0, 0, (angle) * rotSpeed / 100);
            }

        }
    }
}