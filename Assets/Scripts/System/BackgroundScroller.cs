using UnityEngine;

namespace Assets.Scripts.System
{
    public class BackgroundScroller : MonoBehaviour
    {
        public float Parralax = 2f;
        public GameObject Player;

        private void Update()
        {
            MeshRenderer background = GetComponent<MeshRenderer>();
            Material mat = background.material;
            Vector2 offset = mat.mainTextureOffset;
            {
                offset.x = this.transform.position.x / transform.localScale.x / Parralax;
                offset.y = this.transform.position.y / transform.localScale.y / Parralax;
            }

            mat.mainTextureOffset = offset;
        }
    }
}