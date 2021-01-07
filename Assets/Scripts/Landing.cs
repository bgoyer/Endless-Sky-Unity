using UnityEngine;

public class Landing : MonoBehaviour
{
    public GameObject LandingTip;
    public GameObject Player;
    public GameObject LandingScreen;

    private bool canLand = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LandableBody"))
        {
            LandingTip.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Player.GetComponent<PlayerMovement>().canControl = false;
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