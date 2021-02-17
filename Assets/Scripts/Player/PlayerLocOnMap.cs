using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLocOnMap : MonoBehaviour
    {
        public GameObject ship;

    
        void Update()
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2((ship.transform.position.x) / 15f, (ship.transform.position.y) / 15f); 
        }
    }
}
