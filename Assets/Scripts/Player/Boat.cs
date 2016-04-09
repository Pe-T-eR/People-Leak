using UnityEngine;
using Assets.Scripts.Configuration;

public class Boat : MonoBehaviour {

    public int Capacity;
    public int MaxSpeed;
    public double RotationSpeed;
    public int Score;

	// Use this for initialization
	void Start () {

        Capacity = Constants.DefaultValues.BaseCapacity;
        MaxSpeed = Constants.DefaultValues.BaseSpeed;
        RotationSpeed = Constants.DefaultValues.BaseRotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
