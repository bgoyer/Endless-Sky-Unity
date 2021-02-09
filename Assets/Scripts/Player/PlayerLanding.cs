using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerLanding : MonoBehaviour
    {
        public GameObject LandingTip;
        public GameObject LandingScreen;
    
        private GameObject Player;
        private KeyMap keyMap;
        private int currentFuel;
        private int maxFuel;
        private GameObject sceneScripts;

        private void Start()
        {
            sceneScripts = GameObject.Find("/SceneScripts");
            Player = this.gameObject;
            keyMap = sceneScripts.GetComponent<KeyMap>();
            currentFuel = transform.GetChild(0).GetComponent<ShipVariables>().HyperdriveFuel;
            maxFuel = transform.GetChild(0).GetComponent<ShipVariables>().MaxHyperdriveFuel;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("LandableBody"))
            {
                print("collided with planet");
                LandingTip.GetComponent<Text>().text = $"Press {keyMap.Land} to land";
                
                if (Input.GetKeyDown(keyMap.Land))
                {
                    currentFuel = maxFuel;
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    Player.GetComponent<ShipVariables>().CanControl = false;
                    LandingScreen.SetActive(true);
                    sceneScripts.GetComponent<GameController>().Pause();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("LandableBody"))
            {
                LandingTip.GetComponent<Text>().text = "";
            }
        }
    }
}