using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerStats : MonoBehaviour
{
    public GameObject Player;
    public Text Credits;
    private Player Stats;

    private void Start()
    {
       
    }

    private void Update()
    {
        Credits.text = Player.GetComponent<Player>().Credits +" credits";
    }
}