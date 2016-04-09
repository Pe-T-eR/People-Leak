using System.Collections.Generic;
using Assets.Scripts.Dock;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class AfricanDock : Dock
{

    private Dictionary<GameObject, float> _waitDictionary;
    private const long WaitTimeBetweenAdd = 1;

    new void Start()
    {
        base.Start();
        _waitDictionary = new Dictionary<GameObject, float>();
    }

	// Update is called once per frame
	void Update ()
	{
	    foreach (var ship in DockedShips)
	    {
	        float time;
	        _waitDictionary.TryGetValue(ship, out time);

	        if (!(time < Time.time)) continue;

	        Debug.Log("Adding refugee to Player: " + ship.name);
	        _waitDictionary[ship] = Time.time + WaitTimeBetweenAdd;
	    }
	}
}
