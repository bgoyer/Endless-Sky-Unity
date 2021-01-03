using ES.Data.Models.Player;
using ES.Data.Services;
using System;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    PlayerService _service = new PlayerService();

    // Start is called before the first frame update
    public void Load()
    {
        try
        {
            // Create a new player
            PlayerModel player = new PlayerModel();
            player.Name = "Brendan";
            player.Location.X = -216;
            player.Location.Y = -216;
            player = _service.Insert(player);

            PlayerModel brendan = _service.Get("Brendan");
            print(brendan.Name);
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
