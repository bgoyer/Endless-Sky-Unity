using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ThrusterController : MonoBehaviour
    {
        public float Acceleration = 100f;
        public double MaxSpeed = 10;
        private Rigidbody2D r2D;

        public void Accelerate(GameObject ship)
        {
            r2D = ship.GetComponent<Rigidbody2D>();
            if (ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= MaxSpeed)
            {
                r2D.AddRelativeForce(Vector3.up * Acceleration);
            }
            else
            {
                ship.transform.GetComponent<Rigidbody2D>().velocity *= .999f;
            }
        }
    }
}
