using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ThrusterController : MonoBehaviour
    {
        public double Acceleration = 10;
        public double MaxSpeed = 10;
        private bool canControl;
        private Rigidbody2D r2D;

        public void Accelerate(GameObject ship)
        {
            canControl = ship.gameObject.GetComponent<ShipVariables>().CanControl;
            r2D = ship.GetComponent<Rigidbody2D>();
            if (canControl == true)
            {
                if (ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= MaxSpeed)
                {
                    r2D.AddRelativeForce(Vector3.up * (float)Acceleration);
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    ship.transform.GetComponent<Rigidbody2D>().velocity *= .999f;
                }
            }
        }
    }
}
