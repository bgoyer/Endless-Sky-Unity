using UnityEngine;

namespace Assets.Scripts.Player
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform ship;
        public GameObject player;
        private void Start()
        {
        }

        private void Update()
        {
            if (player.transform.childCount > 0)
            {
                ship = player.transform.GetChild(0).transform;
                this.transform.position = new Vector3(ship.position.x, ship.position.y, -10);
            }
        }
    }
}