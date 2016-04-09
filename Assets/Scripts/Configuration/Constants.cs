namespace Assets.Scripts.Configuration
{
    public class Constants  {

        public class Tags
        {
            public const string Player = "Player";
            public const string Land = "Land";
            public const string Wall = "Wall";
        }

        public class DefaultValues
        {
            //Boat
            public const int BaseSpeed = 50;
            public const double BaseRotationSpeed = 76.2;
            public const int BaseCapacity = 10;

            //Refugee
            public const int RefugeeValue = 10;

            // Dock
            public const float WaitTimeBetweenShipAdd = 1f;
            public const float WaitTimeBetweenDockAdd = 5f;
            public const float WaitTimeBetweenDockRemove = 3f;
        }
    }
}
