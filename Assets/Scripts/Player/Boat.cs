using UnityEngine;
using Assets.Scripts.Configuration;
using System.Collections;

public class Boat : MonoBehaviour {

    public int Capacity;
    public int MaxSpeed;
    public double RotationSpeed;

    private RefugeeContainer container;

	// Use this for initialization
	void Start () {

        Capacity = Constants.DefaultValues.BoatBaseCapacity;
        MaxSpeed = Constants.DefaultValues.BoatBaseSpeed;
        RotationSpeed = Constants.DefaultValues.BoatBaseRotationSpeed;

        container = GetComponentInParent<RefugeeContainer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
