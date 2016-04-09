using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Controls : MonoBehaviour {

        [Tooltip("Keycode for moving left")]
        public string LeftKey;
        [Tooltip("Keycode for moving right")]
        public string RightKey;
        [Tooltip("Keycode for dumping refugees")]
        public string DumpKey;
        [Tooltip("Defines a realtive rotation speed")]
        public float RoationSpeed;
        [Tooltip("Defines a relative movement speed")]
        public float MovementSpeed;
        [Tooltip("How close the boat can get to a blocking element")]
        public float RayDistance;
        

        private Rigidbody rb;
        private Boat boat;
        private AudioHandler audioHandler;

        // Use this for initialization
        void Start () {
            // Default movement
            LeftKey = string.IsNullOrEmpty(LeftKey) ? "a" : LeftKey;
            RightKey = string.IsNullOrEmpty(RightKey) ? "d" : RightKey;
            DumpKey = string.IsNullOrEmpty(DumpKey) ? "space" : DumpKey;
            rb = GetComponent<Rigidbody>();
            boat = GetComponent<Boat>();
            audioHandler.GetComponent<AudioHandler>();
        }
	
        // Update is called once per frame
        void Update()
        {
            // Turning
            if (Input.GetKey(LeftKey))
                transform.Rotate(Vector3.up, -RoationSpeed*Time.deltaTime);
            else if (Input.GetKey(RightKey))
                transform.Rotate(Vector3.up, RoationSpeed*Time.deltaTime);

            if(Input.GetKey(DumpKey))
            {
                var refugee = boat.RefugeeContainer.RemoveRefugee();

                if(refugee != null)
                {
                    refugee.Dump();

                    audioHandler.Play(audioHandler.DumpSound);

                    //Todo: Add small boost
                }
            }

            // Forward movement
            rb.velocity = transform.forward * MovementSpeed * Time.deltaTime;
        }
    }
}
