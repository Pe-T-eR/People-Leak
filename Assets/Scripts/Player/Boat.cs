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
    private GameMaster _gameMaster;

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
        _gameMaster = FindObjectOfType<GameMaster>();
	}
	
	public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.Tags.Player))
        {
            var impactForce = CalculateImpact(other.GetComponentInParent<Controls>());
            BoatCollision(impactForce);
        }
        else if(other.CompareTag(Constants.Tags.CoastGuard))
        {

        }
        else if(other.CompareTag(Constants.Tags.Refugee))
        {
            RefugeeCollision(other);
        }
    }

    private void BoatCollision(float impactForce)
    {
        if(impactForce > 1f)
        {
            var audioHandler = _gameMaster.GetComponent<AudioHandler>();
            audioHandler.Play(audioHandler.CollisionSound);

            if(IsRefugeeDropped(impactForce))
            {
                var droppedRefugee = RefugeeContainer.RemoveRefugee();

                if(droppedRefugee != null)
                {
                    droppedRefugee.Dump();
                }
            }
        }
    }

    private void RefugeeCollision(Collider other)
    {
        var refugee = other.GetComponentInParent<Refugee>();

        if (RefugeeContainer.TryAddRefugee(refugee))
        {
            refugee.PickUp();
        }        
    }

    private float CalculateImpact(Controls other)
    {
        var impactVector = GetComponentInParent<Controls>().GetVelocity() - other.GetVelocity();
        Debug.Log(impactVector.magnitude);
        return impactVector.magnitude;
    }

    private float CalculateImpact()
    {
        return 0f;
    }

    private bool IsRefugeeDropped(float impact)
    {
        var rand = Random.Range(0f, Capacity) * impact;
        return rand >= RefugeeContainer.GetCount();        
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
