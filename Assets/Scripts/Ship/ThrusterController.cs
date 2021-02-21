using UnityEngine;

namespace Assets.Scripts.Ship
{

    public class ThrusterController : MonoBehaviour
    {
        private GameObject thrusterA;
        private GameObject thrusterB;
        private Rigidbody2D r2D;

        public void Accelerate(GameObject ship)
        {
            if (!ship.transform.GetComponent<ShipVariables>().CanControl) {return;}
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(true);
            thrusterB.transform.GetChild(0).gameObject.SetActive(true);
            r2D = ship.GetComponent<Rigidbody2D>();
            if (ship.transform.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= thrusterA.transform.parent.parent.GetComponent<ThrusterVariables>().MaxSpeed)
            {
                r2D.AddRelativeForce(Vector3.up * (float)thrusterA.transform.parent.parent.GetComponent<ThrusterVariables>().ThrustEnergy);
            }
            else
            {
                ship.transform.GetComponent<Rigidbody2D>().velocity *= .999f;
            }
        }

        public void StopThruster(GameObject ship)
        {
            thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
            thrusterA.transform.GetChild(0).gameObject.SetActive(false);
            thrusterB.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
