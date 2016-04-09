using UnityEngine;
using Assets.Scripts.Dock;
using System.Collections.Generic;

public class UpgradeCapacityDock : Dock {

    private Dictionary<Boat, float> _waitDictionary;

    // Use this for initialization
	new void Start () {

        base.Start();
        _waitDictionary = new Dictionary<Boat, float>();
	}

    // Update is called once per frame
    void Update()
    {

        foreach (var ship in DockedBoats)
        {
            float time;
            _waitDictionary.TryGetValue(ship, out time);

            if (!(time < Time.time)) continue;
        }
    }
}
