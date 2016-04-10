using UnityEngine;
using Assets.Scripts.Configuration;

namespace Assets.Scripts.Player
{
    public class Controls : MonoBehaviour {

        [Tooltip("Keycode for moving left")]
        public string LeftKey;
        [Tooltip("Keycode for moving right")]
        public string RightKey;
		[Tooltip("Keycode for moving forward")]
		public string ForwardKey;
		[Tooltip("Keycode for moving backward")]
		public string BackwardKey;
        [Tooltip("Keycode for dumping refugees")]
        public string DumpKey;
        [Tooltip("Defines a realtive rotation speed")]
        public float RoationSpeed;
        [Tooltip("Defines a relative movement speed")]
        public float MovementSpeed;
		[Tooltip("Defines a relative movement speed when moving backward")]
		public float MovementSpeedBackward;
		[Tooltip("Defines the boat drag")]
		public float Drag;
        [Tooltip("How close the boat can get to a blocking element")]
        public float RayDistance;
		[Tooltip("The minimum velocity for the boat to turn")]
		public float MinimumTurnVelocity;

        public Transform DropPoint { get; set; }


        private Rigidbody _rb;
        private Boat _boat;

        private float _lastDump;
        private bool _dumpBoost;

        // Use this for initialization
        void Start () {
            // Default movement
            LeftKey = string.IsNullOrEmpty(LeftKey) ? "a" : LeftKey;
            RightKey = string.IsNullOrEmpty(RightKey) ? "d" : RightKey;
			ForwardKey = string.IsNullOrEmpty(ForwardKey) ? "w" : ForwardKey;
			BackwardKey = string.IsNullOrEmpty(BackwardKey) ? "s" : BackwardKey;
            DumpKey = string.IsNullOrEmpty(DumpKey) ? "space" : DumpKey;
            _rb = GetComponent<Rigidbody>();
            _boat = GetComponent<Boat>();
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
			var direction = Input.GetKey(ForwardKey) ? 1.0f : Input.GetKey(BackwardKey) ? - MovementSpeedBackward : 0.0f;

            if(Input.GetKey(DumpKey) && Time.time > Constants.DefaultValues.DumpCooldown + _lastDump)
            {
                var refugee = _boat.RefugeeContainer.RemoveRefugee();

                if(refugee != null)
                {
                    var dropPos = transform.position + transform.forward * -2 + new Vector3(0,0.6f,0);

                    refugee.Dump(dropPos);
                    _lastDump = Time.time;
                    _dumpBoost = true;
                }
            }

            if(Time.time > _lastDump + Constants.DefaultValues.DumpDuration)
            {
                _dumpBoost = false;
            }

            var dumpBoost = _dumpBoost ? 1f + CalculateBoost() : 1f;

			_rb.velocity = transform.forward.normalized * _rb.velocity.magnitude;
			_rb.AddForce(transform.forward.normalized * MovementSpeed * dumpBoost * Time.deltaTime * direction);
			var drag = _rb.velocity.magnitude * _rb.velocity.magnitude * Drag * Time.deltaTime;
			_rb.AddForce(transform.forward.normalized * drag * -1);
        }

        private float CalculateBoost()
        {
            var timeSinceDump = Time.time - _lastDump;
            var remaining = (Constants.DefaultValues.DumpDuration - timeSinceDump) / Constants.DefaultValues.DumpDuration;

            return Constants.DefaultValues.DumpBoost * remaining;
        }

        public Vector3 GetVelocity()
        {
            return _rb.velocity;
        }
    }
}
