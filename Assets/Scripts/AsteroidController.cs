using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsteroidController : MonoBehaviour
    {
        public int Health = 100;
        public int MatAmountPer = 10;
        public string AsteriodType = "Alunimum";
        public GameObject Flotsam;
        public GameObject GarbageHolder;
        private double rotateSpeed;

        private void Start()
        {
            GarbageHolder = GameObject.Find("/GarbageHolder");
            Flotsam = UnityEngine.Resources.Load<GameObject>("Prefabs/Asteroid/Flotsams/Aluminumflotasam");
            rotateSpeed = Random.Range(1, 3);
        }

        private void OnTriggerEnter2D(Component collision)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                Health -= collision.gameObject.GetComponent<BulletController>().damage;
                collision.gameObject.GetComponent<BulletController>().OnHit();
            }
        }

        private void Update()
        {
            StartCoroutine("Rotate");
            if (Health <= 0)
            {
                GameObject _flotsam = Instantiate(Flotsam, GarbageHolder.transform);
                _flotsam.transform.position = this.transform.position;
                _flotsam.GetComponent<FlotsamController>().matAmount = MatAmountPer;
                _flotsam.GetComponent<FlotsamController>().matType = AsteriodType;
                Destroy(this.gameObject);
            }
        }

        private IEnumerator Rotate()
        {
            
            if (!this.GetComponent<SpriteRenderer>().isVisible) yield break;
            transform.Rotate(0, 0, 50 * Time.deltaTime * (float)rotateSpeed, Space.Self);
            yield return new WaitForSeconds(.01f);
        }
    }
}