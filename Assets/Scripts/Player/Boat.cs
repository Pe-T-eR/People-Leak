using UnityEngine;
using Assets.Scripts.Configuration;
using Assets.Scripts.Player;

public class Boat : MonoBehaviour {

    public int Capacity;
    public float MaxSpeed {
        get { return _movementControls.MovementSpeed; }
        set { _movementControls.MovementSpeed = value; }
    }
    public double RotationSpeed;
    public float AddSpeedDelay;
    public int Score;

    public int CapacityLevel;
    public int EngineLevel;
    public int AddSpeedLevel;

    public RefugeeContainer RefugeeContainer;
    private Controls _movementControls;

	public GameObject[] UpgradeBodies;

	// Use this for initialization
	void Start ()
	{
	    _movementControls = GetComponent<Controls>();
        Capacity = Constants.DefaultValues.BoatBaseCapacity;
        MaxSpeed = Constants.DefaultValues.BoatBaseSpeed;
        RotationSpeed = Constants.DefaultValues.BoatBaseRotationSpeed;

        CapacityLevel = 1;
        EngineLevel = 1;
        AddSpeedDelay = 1f;

        RefugeeContainer = GetComponentInParent<RefugeeContainer>();
		ShowEngineUpgrade(EngineLevel);
	}
	
	public void OnTriggerEnter2D()
    {

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

    public void UpgradeAddSpeed()
    {
        if( Score >= AddSpeedLevel * Constants.DefaultValues.AddSpeedUpgradeCostModifier)
        {
            Score -= AddSpeedLevel * Constants.DefaultValues.AddSpeedUpgradeCostModifier;
            AddSpeedDelay *= Constants.DefaultValues.AddSpeedUpgradeEffet;
            AddSpeedLevel++;
        }
    }

    public void UpgradeEngine()
    {
		if (Score >= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier && EngineLevel < UpgradeBodies.Length)
        {
            Score -= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier;
            MaxSpeed += Constants.DefaultValues.EngineUpgradeEffect;
            EngineLevel++;
			ShowEngineUpgrade(EngineLevel);
        }
    }

	protected void ShowEngineUpgrade(int level) {
		for (var i = 0; i < UpgradeBodies.Length; i++) {
			var upgradeBody = UpgradeBodies[i];
			if (upgradeBody == null) {
				continue;
			}
			var renderers = upgradeBody.GetComponentsInChildren<Renderer>();
			foreach (var renderer in renderers) {
				renderer.enabled = i == level - 1;
			}
		}
	}
}
