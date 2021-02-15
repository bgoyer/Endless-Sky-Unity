using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLocOnMap : MonoBehaviour
    {
        public GameObject ship;
    
        void Update()
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2((ship.transform.position.x + 1) / 14.7f, (ship.transform.position.y + 1) / 14.7f); 
        }
    }
}
