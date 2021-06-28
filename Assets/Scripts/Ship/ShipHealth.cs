using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipHealth : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<BulletController>() == null)
            {
                return;
            }
            if (col.GetComponent<BulletController>().ParentShip != gameObject)
            {
                this.transform.parent.GetComponent<AIController>().Attacked(col);
            }
        }
    }
}