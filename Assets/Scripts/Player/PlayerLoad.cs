using System;
using Assets.Resources.Data.ModelData.Models.Player;
using Assets.Resources.Data.ModelData.Services;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLoad : MonoBehaviour
    {
        PlayerService service = new PlayerService("en");

        // Start is called before the first frame update
        public void Load()
        {
            try
            {
                // Create a new player
                PlayerModel player = service.NewModel();
                player.Name = "Brendan";
                player.Location.X = -216;
                player.Location.Y = -216;

                if (!service.Exists(player)) {
                    player = service.Save(player);
                }

                PlayerModel brendan = service.Get(player.Id);
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
