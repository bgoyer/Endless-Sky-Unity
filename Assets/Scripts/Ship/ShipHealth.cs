using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipHealth : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (this.GetComponent<ShipVariables>().HullHp <= 0)
            {

            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "AIBullet" || col.tag == "PlayerBullet")
            {
                this.transform.parent.GetComponent<AIController>().Target = col.GetComponent<BulletController>().ParentShip;
                this.transform.parent.GetComponent<AIController>().Hit(col);
            }
        }
    }
}
