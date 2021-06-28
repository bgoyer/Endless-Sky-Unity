using System.Collections;
using UnityEngine;

namespace Assets.Scripts.System
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

        private void Update()
        {
            StartCoroutine("Rotate");
            if (Health <= 0)
            {
                GameObject flotsam = Instantiate(Flotsam, GarbageHolder.transform);
                flotsam.transform.position = this.transform.position;
                flotsam.GetComponent<FlotsamController>().MatAmount = MatAmountPer;
                flotsam.GetComponent<FlotsamController>().MatType = AsteriodType;
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