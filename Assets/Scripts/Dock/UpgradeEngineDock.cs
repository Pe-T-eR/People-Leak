using UnityEngine;
using Assets.Scripts.Dock;
using Assets.Scripts.Configuration;
using System.Collections.Generic;

public class UpgradeEngineDock : Dock
{
    private Dictionary<Boat, float> _waitDictionary;
    public GameObject Welder;

    private AudioSource _welderAudio;
    private Animation _welderAnimation;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        _waitDictionary = new Dictionary<Boat, float>();
        _welderAudio = Welder.GetComponent<AudioSource>();
        _welderAnimation = Welder.GetComponent<Animation>();
    }

    new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Constants.Tags.Player))
        {
            var boat = other.GetComponent<Boat>();
            _waitDictionary[boat] = Time.time + Constants.DefaultValues.WaitForEngineUpgrade * boat.EngineLevel;
        }
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
                _waitDictionary[boat] = Time.time + Constants.DefaultValues.WaitForEngineUpgrade * boat.EngineLevel;
                continue;
            }

            //We haven't waited long enough or can't afford the upgrade
            if (time > Time.time) continue;
            if (boat.Score < boat.EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier) continue;

            //Wohoo, upgrade time!
            Debug.Log("Upgrading...");
            boat.UpgradeEngine();
            _welderAnimation.Play();
            _welderAudio.Play();

            //Wait for next upgrade
            _waitDictionary[boat] = Time.time + Constants.DefaultValues.WaitForEngineUpgrade * boat.EngineLevel;
        }
    }
}
