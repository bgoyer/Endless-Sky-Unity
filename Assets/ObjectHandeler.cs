using System.Collections;
using UnityEngine;

namespace Assets
{
    public class ObjectHandeler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.CompareTag("PlayerShip") || col.transform.CompareTag("AIShip"))
            {
                col.transform.parent.SetParent(transform.GetChild(0));
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.transform.CompareTag("PlayerShip") || col.transform.CompareTag("AIShip"))
            {
                StartCoroutine(MoveObjects(col));
            }
        }

        private IEnumerator MoveObjects(Collider2D col)
        {
            yield return new WaitForSeconds(1);
            col.transform.parent.SetParent(transform.parent.GetChild(0));
        }
    }
}