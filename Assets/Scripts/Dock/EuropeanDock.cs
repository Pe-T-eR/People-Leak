using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Dock
{
    public class EuropeanDock : Dock
    {
        private Dictionary<RefugeeContainer, float> _waitDictionary;
        private GameMaster _gameMaster;
        private AudioHandler _audioHandler;

        public Color DockColor;
		public GameObject Billboard;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            _waitDictionary = new Dictionary<RefugeeContainer, float>();
            _gameMaster = FindObjectOfType<GameMaster>();
            _audioHandler = _gameMaster.GetComponent<AudioHandler>();

            if (Billboard != null) {
                foreach (var r in Billboard.GetComponentsInChildren<Renderer>())
                { 
				    r.material.color = DockColor;
                }
			}
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var ship in DockedBoats)
            {
                var container = ship.GetComponent<RefugeeContainer>();

                float time;
                _waitDictionary.TryGetValue(container, out time);

                if (!(time < Time.time)) continue;

                var r = container.RemoveRefugee(this);
                if(r == null) continue;

                _audioHandler.Play(_audioHandler.RefugeeDelivered);
                                
                _waitDictionary[container] = Time.time + Constants.DefaultValues.WaitTimeBetweenDockRemove;
                ship.gameObject.GetComponent<Boat>().Score += r.Value;
                Destroy(r.gameObject);
            }
        }
    }
}
