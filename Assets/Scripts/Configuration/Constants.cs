namespace Assets.Scripts.Configuration
{
    public class Constants  {

        public class Tags
        {
            public const string Player = "Player";
            public const string Land = "Land";
            public const string Wall = "Wall";
            public const string Music = "Music";
            public const string CoastGuard = "Coast Guard";
            public const string Refugee = "Refugee";
        }

        public class DefaultValues
        {
            //Boat
            public const int BoatBaseSpeed = 100;
            public const double BoatBaseRotationSpeed = 76.2;
            public const int BoatBaseCapacity = 10;

            //Refugee
            public const int RefugeeValue = 10;
            public const double RefugeeLifespan = 30f;

            public const double DumpCooldown = 1f;
            public const float DumpBoost = 0.5f;
            public const float DumpDuration = 5f;

            // Dock
            public const float WaitTimeBetweenShipAdd = 1f;
            public const float WaitTimeBetweenDockAdd = 5f;
            public const float WaitTimeBetweenDockRemove = 3f;

            //Upgrade
            public const float WaitForCapacityUpgrade = 2f;
            public const float WaitForEngineUpgrade = 2f;
            public const float WaitForAddSpeedUpgrade = 2f;

            public const int CapacityUpgradeCostModifier = 10;
            public const int EngineUpgradeCostModifier = 10;
            public const int AddSpeedUpgradeCostModifier = 10;

            public const int CapacityUpgradeEffect = 5;
            public const int EngineUpgradeEffect = 100;
            public const float AddSpeedUpgradeEffet = 0.3f;      
            
            // Coast guard
            public const float TimeBetweenRescue = 0.5f;
        }
    }
}
