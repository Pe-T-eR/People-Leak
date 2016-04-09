using UnityEngine;
using Assets.Scripts.Configuration;
using System.Collections;

public class Refugee : MonoBehaviour {

    public int Value;
    public bool Drowning;
    public bool Alive;

    private double lifetime;

	// Use this for initialization
	void Start () {

        Value = Constants.DefaultValues.RefugeeValue;
        lifetime = Constants.DefaultValues.RefugeeLifespan;
        Drowning = false;

	}
	
	// Update is called once per frame
	void Update () {

        //Only update the living
        if (Alive && Drowning)
        {
            //Decrease liftime
            lifetime -= Time.deltaTime;

            //Are we dead yet?
            if (lifetime <= 0)
            {
                Alive = false;
            }
        }
	}

    /// <summary>
    /// Tells the refugee that he has been dumped of the boat, you heartless bastard.
    /// </summary>
    void Dump()
    {
        Drowning = true;
    }

    /// <summary>
    /// Safe a refugee from the sea by inviting on board. What a nice person you are.
    /// </summary>
    void PickUp()
    {
        Drowning = false;
    }
}
