using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerLanding : MonoBehaviour
    {
        public GameObject LandingTip;
        public GameObject LandingScreen;

        private GameObject player;
        private KeyMap keyMap;
        private int currentFuel;
        private int maxFuel;
        private GameObject sceneScripts;

        private void Start()
        {
            sceneScripts = GameObject.Find("/SceneScripts");
            player = this.gameObject;
            keyMap = sceneScripts.GetComponent<KeyMap>();
            currentFuel = this.transform.GetComponent<ShipVariables>().HyperdriveFuel;
            maxFuel = this.transform.GetComponent<ShipVariables>().MaxHyperdriveFuel;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("HabitableBody"))
            {
                LandingTip.GetComponent<Text>().text = $"Press {keyMap.Land} to land";

                if (Input.GetKeyDown(keyMap.Land))
                {
                    print("land");
                    currentFuel = maxFuel;
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    player.GetComponent<ShipVariables>().CanControl = false;
                    LandingScreen.SetActive(true);
                    sceneScripts.GetComponent<GameController>().Pause();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("HabitableBody"))
            {
                LandingTip.GetComponent<Text>().text = "";
            }
        }
    }
}