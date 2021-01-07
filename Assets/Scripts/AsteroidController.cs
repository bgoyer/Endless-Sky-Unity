using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int health = 100;
    public int matAmountPer = 10;
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
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            GameObject _flotsam = Instantiate(flotsam, garbageHolder.transform);
            _flotsam.transform.position = this.transform.position;
            //_flotsam.GetComponent<FlotsamController>().matAmount =+ matAmountPer;
            Destroy(this.gameObject);
        }
    }
}