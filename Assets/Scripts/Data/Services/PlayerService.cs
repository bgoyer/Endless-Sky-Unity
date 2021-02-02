using Assets.Scripts.Data.Models.Player;

namespace Assets.Scripts.Data.Services
{
    public class PlayerService : ServiceBase<PlayerModel>
    {
        public PlayerService() : base("player")
        {
        }

        public PlayerService(string lang) : base("player", lang)
        {
        }
    }
}