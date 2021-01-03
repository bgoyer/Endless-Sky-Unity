using UnityEngine;

public class SystemHUD : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        print(col.transform.gameObject.name);
    }
}