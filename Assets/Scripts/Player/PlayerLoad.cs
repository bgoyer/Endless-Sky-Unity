using System;
using Assets.Scripts.Data.Models.Player;
using Assets.Scripts.Data.Services;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLoad : MonoBehaviour
    {
        PlayerService _service = new PlayerService("en");

        // Start is called before the first frame update
        public void Load()
        {
            try
            {
                // Create a new player
                PlayerModel player = _service.NewModel();
                player.Name = "Brendan";
                player.Location.X = -216;
                player.Location.Y = -216;

                if (!_service.Exists(player)) {
                    player = _service.Save(player);
                }

                PlayerModel brendan = _service.Get(player.Id);
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
}
