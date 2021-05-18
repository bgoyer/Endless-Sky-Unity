using Assets.Scripts.System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class BulletController : MonoBehaviour
    {
        public float Range = 3f;
        public Rigidbody2D R2D;
        public int Damage = 10;
        public GameObject ParentShip;

        private void Start()
        {
            StartCoroutine(DestroyBullet());
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(Range - .5f);
            this.GetComponent<Animator>().SetBool("Destroy", true);
            yield return new WaitForSeconds(.5f);
            Destroy(this.gameObject);
        }

        public void OnHit(Collider2D col)
        {
            this.GetComponent<Animator>().SetBool("Destroy", true);
            if (col.gameObject.GetComponent<Rigidbody2D>())
            {
                this.R2D.velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            else { this.R2D.velocity = new Vector2(0, 0); }
            Destroy(this.gameObject, .5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "PlayerShip" && this.tag.Equals("AIBullet"))
            {
                if (col.GetComponent<ShipVariables>().ShieldHp > 0)
                {
                    col.GetComponent<ShipVariables>().ShieldHp -= Damage;
                }
                else col.GetComponent<ShipVariables>().HullHp -= Damage;
                OnHit(col);
            }
            if (col.gameObject.tag == "AIShip" && this.tag.Equals("PlayerBullet"))
            {
                if (col.GetComponent<ShipVariables>().ShieldHp > 0)
                {
                    col.GetComponent<ShipVariables>().ShieldHp -= Damage;
                }
                else col.GetComponent<ShipVariables>().HullHp -= Damage;
                OnHit(col);
            }
            if (col.CompareTag("Asteroid"))
            {
                col.GetComponent<AsteroidController>().Health -= Damage;
                OnHit(col);
            }
        }
    }
}