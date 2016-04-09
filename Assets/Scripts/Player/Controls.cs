using System.Linq;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Controls : MonoBehaviour {

        [Tooltip("Keycode for moving left")]
        public string LeftKey;
        [Tooltip("Keycode for movinf right")]
        public string RightKey;
        [Tooltip("Defines a realtive rotation speed")]
        public float RoationSpeed;
        [Tooltip("Defines a relative movement speed")]
        public float MovementSpeed;
        [Tooltip("How close the boat can get to a blocking element")]
        public float RayDistance;
        

        // Use this for initialization
        void Start () {
            // Default movement
            LeftKey = string.IsNullOrEmpty(LeftKey) ? "a" : LeftKey;
            RightKey = string.IsNullOrEmpty(RightKey) ? "d" : RightKey;
        }
	
        // Update is called once per frame
        void Update()
        {
            // Turning
            if (Input.GetKey(LeftKey))
                transform.Rotate(Vector3.forward, RoationSpeed*Time.deltaTime);
            else if (Input.GetKey(RightKey))
                transform.Rotate(Vector3.back, RoationSpeed*Time.deltaTime);

            // Moving forward
            var newPos = transform.position + transform.right*-1*MovementSpeed*Time.deltaTime;

            // Blocking
            var hits = Physics2D.CircleCastAll(transform.position, 0.5f, -transform.right, 0.4f);
            var blockingItems = new[] {Constants.Tags.Land, Constants.Tags.Wall};
            var isBloked = hits.Aggregate(false, (current, hit) => current || hit.collider != null && blockingItems.Contains(hit.transform.tag));
            if (!isBloked)
                transform.position = newPos;
        }
    }
}
