using UnityEngine;
using Assets.Scripts.Dock;
using System.Collections.Generic;
using System.Linq;

public class RefugeeContainer : MonoBehaviour {

    //The maximum refugee capacity of the boat
    public int capacity;

    //Contains the refugees currently on the boat
    private List<Refugee> _refugees;

    //Our parent
    private Boat _parent;

	// Use this for initialization
	void Start () {

        _refugees = new List<Refugee>();

        _parent = GetComponentInParent<Boat>();
        capacity = _parent.Capacity;
	}
	
    /// <summary>
    /// Returns the number of refugees currently sitting safely and varmly in the boat.
    /// </summary>
    /// <returns></returns>
	public int GetCount()
    {
        return _refugees.Count;
    }

    /// <summary>
    /// Attempts to add the given refugee to the container. Adds a default refugee if null is received.
    /// Remember to keep your refugees safe.
    /// </summary>
    /// <param name="refugee"></param>
    /// <returns>An indication of whether the refugee was added.</returns>
    public bool TryAddRefugee(Refugee refugee)
    {
        //Do we have room?
        if (GetCount() < capacity)
        {            
            _refugees.Add(refugee != null ? refugee : new Refugee());

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
    /// Please don't call this method while at sea.
    /// </summary>
    /// <returns></returns>
    public Refugee RemoveRefugee(EuropeanDock destination = null)
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
            var refugee = destination == null ? _refugees[0] :_refugees.FirstOrDefault(r => r.Destination == destination);

            //Remove it from the container
            if (refugee != null)
            {
                _refugees.Remove(refugee);
            }

            //And return it
            return refugee;
        }
    }
}
