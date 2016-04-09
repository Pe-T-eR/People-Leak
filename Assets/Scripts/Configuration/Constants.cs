﻿namespace Assets.Scripts.Configuration
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
            public const int BoatBaseSpeed = 50;
            public const double BoatBaseRotationSpeed = 76.2;
            public const int BoatBaseCapacity = 10;

            //Refugee
            public const int RefugeeValue = 10;
            public const double RefugeeLifespan = 60.0;
        }
    }
}
