using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefugeeContainer : MonoBehaviour {

    //The maximum refugee capacity of the boat
    public int capacity;

    //Contains the refugees currently on the boat
    private List<Refugee> refugees;

    //Our parent
    private Boat parent;

	// Use this for initialization
	void Start () {

        refugees = new List<Refugee>();

        parent = GetComponentInParent<Boat>();
        capacity = parent.Capacity;
	}
	
    /// <summary>
    /// Returns the number of refugees currently on the boat.
    /// </summary>
    /// <returns></returns>
	public int GetCount()
    {
        return refugees.Count;
    }

    /// <summary>
    /// Attempts to add the given refugee to the container. Adds a default refugee if null is received.
    /// </summary>
    /// <param name="refugee"></param>
    /// <returns>An indication of whether the refugee was added.</returns>
    public bool TryAddRefugee(Refugee refugee)
    {
        //Do we have room?
        if (GetCount() < capacity)
        {            
            refugees.Add(refugee != null ? refugee : new Refugee());

            //Refugee was added
            return true;
        }
        else
        {
            //Refugee was not added
            return false;
        }
    }

    /// <summary>
    /// Removes a refugee from the container and returns it. Returns null if no refugees are in the container.
    /// </summary>
    /// <returns></returns>
    public Refugee RemoveRefugee()
    {
        //Are there any refugees?
        if(GetCount() == 0)
        {
            //No, return null
            return null;
        }
        else
        {
            //The refugee to return
            var refugee = refugees[0];

            //Remove it from the container
            refugees.Remove(refugee);

            //And return it
            return refugee;
        }
    }
}
