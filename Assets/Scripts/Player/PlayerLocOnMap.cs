using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLocOnMap : MonoBehaviour
    {
        public GameObject Ship;

    
        void Update()
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2((Ship.transform.position.x) / 75f, (Ship.transform.position.y) / 75f); 
        }
    }
}
