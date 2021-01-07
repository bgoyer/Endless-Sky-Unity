using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject ammo;
    public GameObject bulletHolder;

    public float speed = 200f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ammoclone = Instantiate(ammo, bulletHolder.transform, true);
            ammoclone.transform.position = this.transform.position;
            ammoclone.transform.rotation = this.transform.rotation;
            ammoclone.tag = "PlayerBullet";
            ammoclone.GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        }
    }
}