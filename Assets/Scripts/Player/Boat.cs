using UnityEngine;
using Assets.Scripts.Configuration;
using System.Collections;

public class Boat : MonoBehaviour {

    public int Capacity;
    public int MaxSpeed;
    public double RotationSpeed;

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
