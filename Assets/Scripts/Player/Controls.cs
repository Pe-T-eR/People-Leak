using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    [Tooltip("Keycode for moving left")]
    public string LeftKey;
    [Tooltip("Keycode for movinf right")]
    public string RightKey;
    [Tooltip("Defines a realtive rotation speed")]
    public float RoationSpeed;
    [Tooltip("Defines a relative movement speed")]
    public float MovementSpeed;


    // Use this for initialization
    void Start () {
        // Default movement
	    LeftKey = string.IsNullOrEmpty(LeftKey) ? "a" : LeftKey;
	    RightKey = string.IsNullOrEmpty(RightKey) ? "d" : RightKey;
	}
	
	// Update is called once per frame
	void Update () {
        // Turning
        if (Input.GetKey(LeftKey))
            transform.Rotate(Vector3.forward, RoationSpeed * Time.deltaTime);
        else if (Input.GetKey(RightKey))
            transform.Rotate(Vector3.back, RoationSpeed * Time.deltaTime);

        // Moving forward
	    var newPos = transform.position + transform.up * MovementSpeed * Time.deltaTime;


	    transform.position = newPos;
	}
}
