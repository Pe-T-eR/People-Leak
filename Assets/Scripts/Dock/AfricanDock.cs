using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class AfricanDock : Dock
    {
        public int MaxRefugees;
        public int NumberOfRefugees;

        public GameObject RefugeePrefab;
    
        private Dictionary<RefugeeContainer, float> _waitDictionary;
        private List<Refugee> _refugees; 

        new void Start()
        {
            base.Start();
            _waitDictionary = new Dictionary<RefugeeContainer, float>();
            _refugees = new List<Refugee>();
            StartCoroutine(SpawnRefugees());
        }

        // Update is called once per frame
        void Update ()
        {
            foreach (var ship in DockedShips)
            {
                float time;
                _waitDictionary.TryGetValue(ship, out time);

                if (!(time < Time.time)) continue;
                if (!_refugees.Any()) continue;

                ship.TryAddRefugee(_refugees[0]);
                _refugees.RemoveAt(0);

                _waitDictionary[ship] = Time.time + Constants.DefaultValues.WaitTimeBetweenShipAdd;
                NumberOfRefugees = _refugees.Count;
            }
        }

        private IEnumerator SpawnRefugees()
        {
            while (true)
            {
                if (_refugees.Count < MaxRefugees)
                {
                    _refugees.Add(Instantiate(RefugeePrefab).GetComponent<Refugee>());
                    NumberOfRefugees = _refugees.Count;
                }
                yield return new WaitForSeconds(Constants.DefaultValues.WaitTimeBetweenDockAdd);
            }
        }
    }
}
