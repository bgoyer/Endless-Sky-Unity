using UnityEngine;

namespace Assets.Scripts.Player
{
    public class FollowPlayer : MonoBehaviour
    {
        public int zOffset = -10;
        private Transform ship;
        public GameObject Player;

        private void Start()
        {
        }

        private void Update()
        {
            if (Player.transform.childCount > 0)
            {
                ship = Player.transform.GetChild(0).transform;
                this.transform.position = new Vector3(ship.position.x, ship.position.y, zOffset);
            }
        }
    }
}