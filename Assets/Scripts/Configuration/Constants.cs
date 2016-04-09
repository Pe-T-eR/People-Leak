namespace Assets.Scripts.Configuration
{
    public class Constants  {

        public class Tags
        {
            public const string Player = "Player";
            public const string Land = "Land";
            public const string Wall = "Wall";
            public const string Music = "Music";
        }

        public class DefaultValues
        {
            //Boat
            public const int BoatBaseSpeed = 50;
            public const double BoatBaseRotationSpeed = 76.2;
            public const int BoatBaseCapacity = 10;

            //Refugee
            public const int RefugeeValue = 10;
            public const double RefugeeLifespan = 60.0;

            // Dock
            public const float WaitTimeBetweenShipAdd = 1f;
            public const float WaitTimeBetweenDockAdd = 5f;
            public const float WaitTimeBetweenDockRemove = 3f;

            //Upgrade
            public const float WaitForCapacityUpgrade = 2f;
            public const float WaitForEngineUpgrade = 2f;

            public const int CapacityUpgradeCostModifier = 10;
            public const int EngineUpgradeCostModifier = 10;

            public const int CapacityUpgradeEffect = 5;
            public const int EngineUpgradeEffect = 5;
            
        }
    }
}
