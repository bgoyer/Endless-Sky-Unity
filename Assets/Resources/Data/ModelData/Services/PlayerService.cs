using Assets.Resources.Data.ModelData.Models.Player;

namespace Assets.Resources.Data.ModelData.Services
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