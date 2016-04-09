using UnityEngine;
using Assets.Scripts.Configuration;

public class Boat : MonoBehaviour {

    public int Capacity;
    public int MaxSpeed;
    public double RotationSpeed;
    public int Score;

    public int CapacityLevel;
    public int EngineLevel;

    public RefugeeContainer RefugeeContainer;

	// Use this for initialization
	void Start () {

        Capacity = Constants.DefaultValues.BoatBaseCapacity;
        MaxSpeed = Constants.DefaultValues.BoatBaseSpeed;
        RotationSpeed = Constants.DefaultValues.BoatBaseRotationSpeed;

        CapacityLevel = 1;
        EngineLevel = 1;

        RefugeeContainer = GetComponentInParent<RefugeeContainer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpgradeCapacity()
    {
        if (Score >= CapacityLevel * Constants.DefaultValues.CapacityUpgradeCostModifier)
        {
            Score -= CapacityLevel * Constants.DefaultValues.CapacityUpgradeCostModifier;
            Capacity += Constants.DefaultValues.CapacityUpgradeEffect;
            CapacityLevel++;
        }
    }

    public void UpgradeEngine()
    {
        if (Score >= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier)
        {
            Score -= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier;
            MaxSpeed += Constants.DefaultValues.EngineUpgradeEffect;
            EngineLevel++;
        }
    }
}
