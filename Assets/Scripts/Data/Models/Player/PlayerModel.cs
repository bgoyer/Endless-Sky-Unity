namespace ES.Data.Models.Player
{
    public class PlayerModel : ModelBase
    {
        private Location location;

        public int Credits { get; set; }

        public Location Location
        {
            get
            {
                if (location is null)
                {
                    location = new Location();
                }
                return location;
            }
        }

        // Ship
        //
    }

    public class Location
    {
        public CelestialBodyModel CelestialBody { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
    }
}