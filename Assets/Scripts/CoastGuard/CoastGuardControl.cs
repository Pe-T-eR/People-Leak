using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.CoastGuard
{
    public class CoastGuardControl : Dock.Dock
    {

        public GameObject Route;

        private Dictionary<Boat, float> _waitDictionary;

        private List<Transform> _points;
        private NavMeshAgent _agent;
        private GameMaster _gameMaster;
        private AudioHandler _audioHandler;

        private int _point;
        private int Point
        {
            set
            {
                _point = value % _points.Count;
                _agent.SetDestination(_points[_point].position);
            }
            get { return _point; }
        }

        // Use this for initialization
        new void Start () {
            base.Start();
            // Gather route points
            var pointCount = Route.transform.childCount;
            _points = new List<Transform>();
            for (var i = 0; i < pointCount; i++)
                _points.Add(Route.transform.GetChild(i));
            _agent = GetComponent<NavMeshAgent>();
            Point = 0;

            _waitDictionary = new Dictionary<Boat, float>();
            _gameMaster = FindObjectOfType<GameMaster>();
            _audioHandler = _gameMaster.GetComponent<AudioHandler>();
        }
	
        // Update is called once per frame
        void Update () {
            if (Vector3.Distance(transform.position, _agent.destination) < 1f)
                Point++;

            // Rescue people if possible
            foreach (var boat in DockedBoats)
            {
                float time;
                _waitDictionary.TryGetValue(boat, out time);

                if (!(time < Time.time)) continue;

                var refugee = boat.RefugeeContainer.RemoveRefugee();

                if(refugee != null)
                {                    
                    _audioHandler.Play(_audioHandler.CoastGuardSiren);
                }

                _waitDictionary[boat] = Time.time + Constants.DefaultValues.TimeBetweenRescue;
            }
        }

        public Vector3 GetVelocity()
        {
            return GetComponent<Rigidbody>().velocity;
        }
    }
}
