using UnityEngine;
using Assets.Scripts.Configuration;
using Assets.Scripts.Player;
using Assets.Scripts.Refugee;

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
	}
	
	public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Constants.Tags.Player) || other.CompareTag(Constants.Tags.CoastGuard))
        {
            BoatCollision(other);
        }
        else if(other.CompareTag(Constants.Tags.Refugee))
        {
            RefugeeCollision(other);
        }
    }

    private void BoatCollision(Collider2D other)
    {

    }

    private void RefugeeCollision(Collider2D other)
    {
        var refugee = other.GetComponentInParent<Refugee>();

        if (RefugeeContainer.TryAddRefugee(refugee))
        {
            refugee.PickUp();
        }        
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
        if (Score >= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier)
        {
            Score -= EngineLevel * Constants.DefaultValues.EngineUpgradeCostModifier;
            MaxSpeed += Constants.DefaultValues.EngineUpgradeEffect;
            EngineLevel++;
        }
    }
}
