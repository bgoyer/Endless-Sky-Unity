using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float range = 3f;
    public Rigidbody2D R2D;
    public int damage = 10;

    private void Start()
    {
        Destroy(this.gameObject, range);
    }
}