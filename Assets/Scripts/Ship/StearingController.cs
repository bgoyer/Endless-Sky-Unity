using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class StearingController : MonoBehaviour
    {
        private float rotSpeed;

        private Rigidbody2D r2D;
        private bool active = false;
        public void TurnLeft(GameObject ship)
        {
            rotSpeed = ship.transform.GetChild(2).GetChild(0).GetComponent<StearingVariables>().RotationSpeed;
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(rotSpeed, ForceMode2D.Impulse);
        }
        public void TurnRight(GameObject ship)
        {
            r2D = ship.GetComponent<Rigidbody2D>();
            r2D.AddTorque(-rotSpeed, ForceMode2D.Impulse);
        }

        public IEnumerator RotateNegVel(GameObject ship)
        {
            if (active == false)
            {
                var lastVelVector = r2D.velocity;
                active = true;
                float angle = (Mathf.Atan2(-ship.GetComponent<Rigidbody2D>().velocity.y, -ship.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                for (float i = 0; i <= 1; i += (rotSpeed / 3000))
                {
                    if (lastVelVector != r2D.velocity)
                    {
                        angle = (Mathf.Atan2(-ship.GetComponent<Rigidbody2D>().velocity.y, -ship.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg) - 90;
                        q = Quaternion.AngleAxis(angle, Vector3.forward);
                        i = 0;

                    }
                    ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, q, i);
                    yield return new WaitForSeconds(.01f);
                    KeyMap keyMap = this.GetComponent<KeyMap>();
                    if (Input.GetKeyUp(keyMap.TurnAround))
                    {
                        break;
                    }
                }
                active = false;
            }
        }
    }
}
