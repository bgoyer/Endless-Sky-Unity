using UnityEngine;
using UnityEngine.UI;

public class Landing : MonoBehaviour
{
    public GameObject LandingTip;
    public GameObject LandingScreen;
    
    private GameObject Player;
    private KeyMap keyMap;

    private void Start()
    {
        Player = this.gameObject;
        keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LandableBody"))
        {
            LandingTip.GetComponent<Text>().text = $"Press {keyMap.Land} to land";
            LandingTip.SetActive(true);
            if (Input.GetKeyDown(keyMap.Land))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Player.GetComponent<ShipVariables>().CanControl = false;
                LandingScreen.SetActive(true);
                Player.GetComponent<GameController>().Pause();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LandableBody"))
        {
            LandingTip.SetActive(false);
        }
    }
}