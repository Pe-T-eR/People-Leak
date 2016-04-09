using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class AfricanDock : Dock
    {
        private int _numberOfrefugees;
        public int NumberOfRefugees
        {
            get { return _numberOfrefugees; }
            set
            {
                _numberOfrefugees = value;
                UpdateVisualRefugees();
            }
        }

        public GameObject[] RefugeBodies;
        private int _maxRefugees;

        public GameObject RefugeePrefab;
    
        private Dictionary<RefugeeContainer, float> _waitDictionary;
        private List<Refugee> _refugees; 

        new void Start()
        {
            base.Start();
            _waitDictionary = new Dictionary<RefugeeContainer, float>();
            _maxRefugees = RefugeBodies.Length;
            _refugees = new List<Refugee>();
            StartCoroutine(SpawnRefugees());
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
                if (!_refugees.Any()) continue;

                container.TryAddRefugee(_refugees[0]);
                _refugees.RemoveAt(0);

                _waitDictionary[container] = Time.time + Constants.DefaultValues.WaitTimeBetweenShipAdd;
                NumberOfRefugees = _refugees.Count;
            }
        }

        private IEnumerator SpawnRefugees()
        {
            while (true)
            {
                if (_refugees.Count < _maxRefugees)
                {
                    _refugees.Add(Instantiate(RefugeePrefab).GetComponent<Refugee>());
                    NumberOfRefugees = _refugees.Count;
                }
                yield return new WaitForSeconds(Constants.DefaultValues.WaitTimeBetweenDockAdd);
            }
        }

        private void UpdateVisualRefugees()
        {
            for (int i = 0; i < _maxRefugees; i++)
            {
                RefugeBodies[i].SetActive(i < _numberOfrefugees);
            }
        }
    }
}
