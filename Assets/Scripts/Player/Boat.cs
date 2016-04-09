using UnityEngine;
using Assets.Scripts.Configuration;

public class Boat : MonoBehaviour {

    public int Capacity;
    public int MaxSpeed;
    public double RotationSpeed;
    public int Score;

	// Use this for initialization
	void Start () {

        Capacity = Constants.DefaultValues.BoatBaseCapacity;
        MaxSpeed = Constants.DefaultValues.BoatBaseSpeed;
        RotationSpeed = Constants.DefaultValues.BoatBaseRotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
