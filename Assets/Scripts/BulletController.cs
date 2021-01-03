using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D R2D;

    private void Start()
    {
        R2D.AddRelativeForce(transform.up * speed);
        Destroy(this.gameObject, 2f);
    }
}