using UnityEngine;
using Assets.Scripts.Configuration;

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
        

        private Rigidbody _rb;
        private Boat _boat;
        private AudioHandler _audioHandler;

        private float _lastDump;
        private bool _dumpBoost;

        // Use this for initialization
        void Start () {
            // Default movement
            LeftKey = string.IsNullOrEmpty(LeftKey) ? "a" : LeftKey;
            RightKey = string.IsNullOrEmpty(RightKey) ? "d" : RightKey;
            DumpKey = string.IsNullOrEmpty(DumpKey) ? "space" : DumpKey;
            _rb = GetComponent<Rigidbody>();
            _boat = GetComponent<Boat>();
            _audioHandler = GetComponent<AudioHandler>();
            _lastDump = Time.time;
            _dumpBoost = false;
        }
	
        // Update is called once per frame
        void Update()
        {
            // Turning
            if (Input.GetKey(LeftKey))
                transform.Rotate(Vector3.up, -RoationSpeed*Time.deltaTime);
            else if (Input.GetKey(RightKey))
                transform.Rotate(Vector3.up, RoationSpeed*Time.deltaTime);

            if(Input.GetKey(DumpKey) && Time.time > Constants.DefaultValues.DumpCooldown + _lastDump)
            {
                var refugee = _boat.RefugeeContainer.RemoveRefugee();

                if(refugee != null)
                {
                    refugee.Dump();
                    _lastDump = Time.time;

                    _audioHandler.Play(_audioHandler.DumpSound);

                    _dumpBoost = true;
                }
            }

            if(Time.time > _lastDump + Constants.DefaultValues.DumpDuration)
            {
                _dumpBoost = false;
            }

            var dumpBoost = _dumpBoost ? 1f + CalculateBoost() : 1f;

            // Forward movement
            _rb.velocity = transform.forward * MovementSpeed * dumpBoost * Time.deltaTime;
        }

        private float CalculateBoost()
        {
            var timeSinceDump = Time.time - _lastDump;
            var remaining = (Constants.DefaultValues.DumpDuration - timeSinceDump) / Constants.DefaultValues.DumpDuration;

            return Constants.DefaultValues.DumpBoost * remaining;
        }
    }
}
