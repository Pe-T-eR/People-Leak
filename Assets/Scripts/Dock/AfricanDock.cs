using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configuration;
using Assets.Scripts.Refugee;
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
        private GameMaster _gameMaster;
        private AudioHandler _audioHandler;

        public GameObject RefugeePrefab;

        private Dictionary<RefugeeContainer, float> _waitDictionary;
        private List<Refugee.Refugee> _refugees;

        new void Start()
        {
            base.Start();
            _waitDictionary = new Dictionary<RefugeeContainer, float>();
            _maxRefugees = RefugeBodies.Length;
            _refugees = new List<Refugee.Refugee>();
            _gameMaster = FindObjectOfType<GameMaster>();
            _audioHandler = _gameMaster.GetComponent<AudioHandler>();

            var i = 0;
            while(i < Constants.DefaultValues.NumRefugeesAtStart)
            {
                _refugees.Add(Instantiate(RefugeePrefab).GetComponent<Refugee.Refugee>());
                NumberOfRefugees = _refugees.Count;
                i++;
            }

            StartCoroutine(SpawnRefugees());
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var boat in DockedBoats)
            {
                var container = boat.GetComponent<RefugeeContainer>();

                float time;
                _waitDictionary.TryGetValue(container, out time);

                if (!(time < Time.time)) continue;
                if (!_refugees.Any()) continue;

                if (!container.TryAddRefugee(_refugees[0])) continue;
                _refugees.RemoveAt(0);

                _audioHandler.Play(_audioHandler.PickupSound);

                _waitDictionary[container] = Time.time + Constants.DefaultValues.WaitTimeBetweenShipAdd * boat.AddSpeedDelay;
                NumberOfRefugees = _refugees.Count;
            }
        }

        private IEnumerator SpawnRefugees()
        {
            while (true)
            {
                if (_refugees.Count < _maxRefugees)
                {
                    _refugees.Add(Instantiate(RefugeePrefab).GetComponent<Refugee.Refugee>());
                    NumberOfRefugees = _refugees.Count;
                }
                yield return new WaitForSeconds(Constants.DefaultValues.WaitTimeBetweenDockAdd);
            }
        }

        private void UpdateVisualRefugees()
        {
            for (int i = 0; i < _maxRefugees; i++)
            {
                var active = i < _numberOfrefugees;
                RefugeBodies[i].SetActive(active);
                if (active)
                    RefugeBodies[i].transform.GetChild(0).GetComponent<ColorControl>().SetColor(_refugees[i].Destination.DockColor);
            }
        }
    }
}
