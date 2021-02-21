using Assets.Resources.Data.ModelData.Services;

namespace Assets.Resources.Data.ModelData.Imports.Engines
{
    public static class EnginesImport
    {
        public static void Import()
        {
            EnginesService service = new EnginesService("en");

            if (!service.Exists("Afterburner"))
            {
                var ab = service.NewModel();
                ab.Name = "Afterburner";
                //ab.Plural = "Afterburners";
                ab.Category = "engines";
                ab.Cost = 70000;
                ab.Thumbnail = "outfit/afterburner";
                ab.Mass = 15;
                ab.OutfitSpace = -15;
                ab.EngineCapacity = -15;
                ab.Thrust = 25.0;
                ab.Fuel = 0.5;
                ab.Heat = 10.0;
                ab.Effect = "afterburner";
                ab.Description = "An afterburner works by dumping hyperspace fuel into your engines and igniting it, producing a large amount of thrust. This can be very useful for dodging missiles or briefly escaping from faster opponents, but you must be careful to avoid using up so much fuel that you do not have enough left for hyperspace travel.";
                service.Save(ab);
            }

            if (!service.Exists("Ionic Afterburner"))
            {
                var ionicAb = service.NewModel();
                ionicAb.Name = "Ionic Afterburner";
                //ionic_ab.Plural = "Ionic Afterburners";
                ionicAb.Category = "engines";
                ionicAb.Cost = 340000;
                ionicAb.Thumbnail = "outfit/ionic afterburner";
                ionicAb.Mass = 24;
                ionicAb.OutfitSpace = -24;
                ionicAb.EngineCapacity = -24;
                ionicAb.Thrust = 29.0;
                ionicAb.Fuel = 0.1;
                ionicAb.Energy = 5.1;
                ionicAb.Heat = 12.0;
                ionicAb.Effect = "ionic afterburner";
                ionicAb.Description = "The ionic afterburner was designed by the Syndicate to compensate for the primary weakness of an ordinary afterburner: namely, that it consumes hyperspace fuel at a prodigious rate. Ionic afterburners instead use a small amount of fuel combined with a large burst of energy from your ship's batteries to achieve the same effect.";
                service.Save(ionicAb);
            }

            if (!service.Exists("X1050 Ion Engine"))
            {
                var x1050 = service.NewModel();
                x1050.Name = "X1050 Ion Engine";
                //X1050.Plural = "X1050 Ion Engines";
                x1050.Category = "engines";
                x1050.Cost = 20000;
                x1050.Thumbnail = "outfit/tiny ion engines";
                x1050.Mass = 20;
                x1050.OutfitSpace = -20;
                x1050.EngineCapacity = -20;
                x1050.Turn = 110;
                x1050.TurningEnergy = 0.25;
                x1050.TurningHeat = 0.5;
                x1050.Thrust = 4.0;
                x1050.ThrustingEnergy = (int) 0.4 * 10;
                x1050.ThrustingHeat = 0.6;
                x1050.FlareSprite = "effect/ion flare/tiny";
                x1050.FrameRate = 1.2;
                x1050.FlareSound = "ion tiny";
                x1050.Description = "When designing the Barb, the Syndicate took the opportunity to integrate their smallest thruster models to create a more compact steering and thrusting engine.The result is not very useful on anything but a fighter, and is only \"adequate\" in that role. Still, if engine space is at a premium, this is the smallest set of engines that will still get you there(eventually).";
                service.Save(x1050);
            }

            if (!service.Exists("X1700 Ion Thruster"))
            {
                var x1050 = service.NewModel();
                x1050.Name = "X1700 Ion Thruster";
                //X1050.Plural = "X1700 Ion Thrusters";
                x1050.Category = "engines";
                x1050.Cost = 12000;
                x1050.Thumbnail = "outfit/tiny ion thruster";
                x1050.Mass = 16;
                x1050.OutfitSpace = -16;
                x1050.EngineCapacity = -16;
                x1050.Thrust = 6.0;
                x1050.ThrustingEnergy = (int)0.6 * 10;
                x1050.ThrustingHeat = 0.9;
                x1050.FlareSprite = "effect/ion flare/tiny";
                x1050.FrameRate = 1.2;
                x1050.FlareSound = "ion tiny";
                x1050.Description = "This is the smallest thruster that you can buy, so weak that anything larger than a drone will accelerate quite slowly when using this engine. On the other hand, it is also very energy-efficient.  Ion engines consume less energy than plasma engines and produce less heat, but they also take up more space.";
                service.Save(x1050);
            }
        }
    }
}
