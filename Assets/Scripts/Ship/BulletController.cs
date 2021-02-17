using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class BulletController : MonoBehaviour
    {
        public float range = 3f;
        public Rigidbody2D R2D;
        public int damage = 10;
        public GameObject parentShip;

        private void Start()
        {
            StartCoroutine(DestroyBullet());
        }

        IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(range - .5f);
            this.GetComponent<Animator>().SetBool("Destroy", true);
            yield return new WaitForSeconds(.5f);
            Destroy(this.gameObject);
        }

        public void OnHit()
        { 
            this.GetComponent<Animator>().SetBool("Destroy", true);
            this.R2D.velocity = new Vector2(0, 0);
            Destroy(this.gameObject, .5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "PlayerShip" && this.tag.Equals("AIBullet"))
            {
                col.GetComponent<ShipVariables>().HullHP -= damage;
                OnHit();
            }
            if (col.gameObject.tag == "AIShip" && this.tag.Equals("PlayerBullet"))
            {
                col.GetComponent<ShipVariables>().HullHP -= damage;
                OnHit();

            }

        }
    }
}