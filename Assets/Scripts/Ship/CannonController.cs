using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject bulletHolder;
    public float speed = 200f;

    private GameObject ammo;
    private GameObject ship;

    private void Start()
    {
        ammo = Resources.Load<GameObject>("Prefabs/Projectiles/blaster+");
        bulletHolder = GameObject.Find("/GarbageHolder");
        ship = this.transform.parent.parent.parent.parent.gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ammoclone = Instantiate(ammo, bulletHolder.transform, true);
            ammoclone.transform.position = this.transform.position;
            ammoclone.transform.rotation = this.transform.rotation;
            ammoclone.tag = "PlayerBullet";
            ammoclone.GetComponent<Rigidbody2D>().AddForce(transform.up * (speed + ship.GetComponent<Rigidbody2D>().velocity.magnitude));
        }
    }
}