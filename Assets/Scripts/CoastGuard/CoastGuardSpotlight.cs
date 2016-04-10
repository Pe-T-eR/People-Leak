using UnityEngine;
using System.Collections;

public class CoastGuardSpotlight : MonoBehaviour {

    public Light spotlight;

	// Update is called once per frame
	void Update () {
        spotlight.transform.position = transform.position + new Vector3(0, 4, 0);
	}
}
