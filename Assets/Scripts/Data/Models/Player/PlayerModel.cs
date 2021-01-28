namespace ES.Data.Models.Player
{
    public class PlayerModel : ModelBase
    {
        private LocationModel location;

        public int Credits { get; set; }

        public LocationModel Location
        {
            get
            {
                if (location is null)
                {
                    location = new LocationModel();
                }
                return location;
            }
        }

        // Ship
        //
    }

    public class LocationModel
    {
        private CelestialBodyModel celestialBody;

        public CelestialBodyModel CelestialBody
        {
            get
            {
                if (celestialBody is null)
                {
                    celestialBody = new CelestialBodyModel();
                }
                return celestialBody;
            }
        }
        public int Y { get; set; }
        public int X { get; set; }
    }
}