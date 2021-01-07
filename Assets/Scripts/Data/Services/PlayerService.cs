using ES.Data.Models.Player;

namespace ES.Data.Services
{
    public class PlayerService : ServiceBase<PlayerModel>
    {
        public PlayerService() : base("player")
        {
        }
    }
}