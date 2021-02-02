using UnityEngine;

namespace Assets.Scripts
{
    public class Laser : MonoBehaviour
    {
        private void FixedUpdate()
        {
            //Length of the ray
            float laserLength = 50f;
            Vector2 startPosition = (Vector2)transform.position + new Vector2(0.5f, 0.2f);
            int layerMask = LayerMask.GetMask("Default");
            //Get the first object hit by the ray
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, layerMask, 0);

            //If the collider of the object hit is not NUll
            if (hit.collider != null)
            {
                //Hit something, print the tag of the object
                Debug.Log("Hitting: " + hit.collider.tag);
            }

            //Method to draw the ray in scene for debug purpose
            Debug.DrawRay(startPosition, Vector2.right * laserLength, Color.red);
        }
    }
}