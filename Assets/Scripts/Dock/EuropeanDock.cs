using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class EuropeanDock : Dock
    {
        private Dictionary<RefugeeContainer, float> _waitDictionary;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            _waitDictionary = new Dictionary<RefugeeContainer, float>();
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

                var r = container.RemoveRefugee();
                if(r == null) continue;

                ship.gameObject.GetComponent<Boat>().Score += r.Value;
                Destroy(r.gameObject);
            }
        }
    }
}
