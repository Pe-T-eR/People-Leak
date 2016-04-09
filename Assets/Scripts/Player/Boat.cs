using UnityEngine;
using Assets.Scripts.Configuration;
using Assets.Scripts.Player;
using Assets.Scripts.Refugee;
using Assets.Scripts.CoastGuard;

public class Boat : MonoBehaviour {

    public int Capacity;
    public float MaxSpeed {
        get { return _movementControls.MovementSpeed; }
        set { _movementControls.MovementSpeed = value; }
    }
    public double RotationSpeed;
    public float AddSpeedDelay;
    public int Score;

    public GameObject DropPoint;

    public Transform DropPosition {
        get { return _movementControls.DropPoint; }
        set { _movementControls.DropPoint = value; }
    }

    public int CapacityLevel;
    public int EngineLevel;
    public int AddSpeedLevel;

    public RefugeeContainer RefugeeContainer;

    private Controls _movementControls;
    private GameMaster _gameMaster;
    private AudioHandler _audioHandler;

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

	    DropPosition = DropPoint.transform;

		ShowEngineUpgrade(EngineLevel);

        _gameMaster = FindObjectOfType<GameMaster>();
        _audioHandler = _gameMaster.GetComponent<AudioHandler>();
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
            var impactForce = CalculateImpact(other.GetComponent<CoastGuardControl>());
            if(impactForce > 1f)
            {
                _audioHandler.Play(_audioHandler.CollisionSound);
            }
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
                    droppedRefugee.Dump(transform.TransformPoint(DropPosition.position));
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

    private float CalculateImpact(CoastGuardControl other)
    {
        var impactVector = GetComponentInParent<Controls>().GetVelocity() - other.GetVelocity();
        Debug.Log(impactVector.magnitude);
        return impactVector.magnitude;
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

    public void Countdown(float time)
    {

    }

    public void StopCountdown()
    {

    }
}
