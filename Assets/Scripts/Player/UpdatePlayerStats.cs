using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class UpdatePlayerStats : MonoBehaviour
    {
        public GameObject Player;
        public Text Credits;
        private Player stats;

        private void Start()
        {
        }

        private void Update()
        {
            Credits.text = Player.GetComponent<Player>().Credits + " credits";
        }
    }
}