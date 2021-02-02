using UnityEngine;

namespace Assets.Scripts
{
    public class BackgroundScroller : MonoBehaviour
    {
        public float parralax = 2f;

        private void Update()
        {
            MeshRenderer background = GetComponent<MeshRenderer>();
            Material mat = background.material;
            Vector2 offset = mat.mainTextureOffset;

            offset.x = this.transform.position.x / transform.localScale.x / parralax;
            offset.y = this.transform.position.y / transform.localScale.y / parralax;

            mat.mainTextureOffset = offset;
        }
    }
}