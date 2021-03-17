using UnityEngine;

namespace Assets.Scripts.System
{
    public class AsteroidFieldController : MonoBehaviour
    {
        public GameObject AsteroidToInstantiate;
        private CircleCollider2D coll2d;
        private float range;
        private float maxObjects;

        private void Start()
        {
            coll2d = this.gameObject.GetComponent<CircleCollider2D>();
            range = coll2d.radius * .5f;
            maxObjects = Random.Range(10, 30);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerShip"))
            {
                for (int i = 0; i < maxObjects; i++)
                {
                    GameObject asteroidClone = Instantiate(AsteroidToInstantiate, this.transform);
                    asteroidClone.transform.position = new Vector2(this.transform.position.x + Random.Range(-range, range), this.transform.position.y + Random.Range(-range, range));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerShip"))
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Destroy(this.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}