using UnityEngine;

namespace Assets.Scripts.Ship
{

    public class ThrusterController : MonoBehaviour
    {
        private GameObject thrusterA;
        private GameObject thrusterB;
        public float Acceleration = 100f;
        public double MaxSpeed = 10;
        private Rigidbody2D r2D;

        public void Accelerate(GameObject ship)
        {
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(true);
            thrusterB.transform.GetChild(0).gameObject.SetActive(true);
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
