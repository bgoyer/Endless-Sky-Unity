using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class BulletController : MonoBehaviour
    {
        public float range = 3f;
        public Rigidbody2D R2D;
        public int damage = 10;

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
            this.GetComponent<SpriteRenderer>().sprite = null;
            this.GetComponent<Animator>().SetBool("Destroy", true);
            this.R2D.velocity = new Vector2(0, 0);
            Destroy(this.gameObject, .5f);
        } 
    }
}