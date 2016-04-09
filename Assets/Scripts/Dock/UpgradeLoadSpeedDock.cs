using UnityEngine;
using Assets.Scripts.Dock;
using Assets.Scripts.Configuration;
using System.Collections.Generic;

public class UpgradeLoadSpeedDock : Dock
{

    private Dictionary<Boat, float> _waitDictionary;

    // Use this for initialization
    new void Start()
    {

        base.Start();
        _waitDictionary = new Dictionary<Boat, float>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var boat in DockedBoats)
        {
            float time;
            _waitDictionary.TryGetValue(boat, out time);

            //Start waiting if we aren't already
            if (time == default(float))
            {
                _waitDictionary[boat] = Time.time + Constants.DefaultValues.WaitForAddSpeedUpgrade * boat.AddSpeedLevel;
                continue;
            }

            //We haven't waited long enough or can't afford the upgrade
            if (!(time < Time.time)) continue;
            if (!(boat.Score >= boat.AddSpeedLevel * Constants.DefaultValues.AddSpeedUpgradeCostModifier)) continue;

            //Wohoo, upgrade time!
            boat.UpgradeAddSpeed();

            //Wait for next upgrade
            _waitDictionary[boat] = Time.time + Constants.DefaultValues.WaitForAddSpeedUpgrade * boat.AddSpeedLevel;
        }
    }
}
