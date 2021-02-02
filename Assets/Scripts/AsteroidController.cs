using System.Collections;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsteroidController : MonoBehaviour
    {
        public int health = 100;
        public int matAmountPer = 10;
        public string asteriodType = "Alunimum";
        public GameObject flotsam;
        public GameObject garbageHolder;

        private void Start()
        {
            garbageHolder = GameObject.Find("/GarbageHolder");
            flotsam = Resources.Load<GameObject>("Prefabs/Asteroid/Flotsams/Aluminumflotasam");
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                health -= collision.gameObject.GetComponent<BulletController>().damage;
                collision.gameObject.GetComponent<BulletController>().OnHit();
            }
        }

        private void Update()
        {
            StartCoroutine("Rotate");
            if (health <= 0)
            {
                GameObject _flotsam = Instantiate(flotsam, garbageHolder.transform);
                _flotsam.transform.position = this.transform.position;
                _flotsam.GetComponent<FlotsamController>().matAmount = matAmountPer;
                _flotsam.GetComponent<FlotsamController>().matType = asteriodType;
                Destroy(this.gameObject);
            }
        }

        private IEnumerator Rotate()
        {
            if (this.GetComponent<SpriteRenderer>().isVisible)
            {
                transform.Rotate(0, 0, (50) * Time.deltaTime, Space.Self);
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}