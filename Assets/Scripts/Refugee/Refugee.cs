using Assets.Scripts.Configuration;
using Assets.Scripts.Dock;
using UnityEngine;

namespace Assets.Scripts.Refugee
{
    public class Refugee : MonoBehaviour
    {

        public int Value;
        public bool Drowning;

        private GameMaster _gameMaster;
        private EuropeanDock _destination;
        private ColorControl _colorControl;
        private ColorControl ColorControl {
            get {
                return _colorControl ??
                       (_colorControl = gameObject.transform.FindChild("RefugeeBody").GetComponent<ColorControl>());
            }
        }

        public EuropeanDock Destination
        {
            get
            {
                if (_destination == null)
                {
                    var destinations = FindObjectsOfType<EuropeanDock>();
                    _destination = destinations[Random.Range(0, destinations.Length)];
                    ColorControl.SetColor(_destination.DockColor);
                }
                return _destination;
            }
        }

        private double _lifetime;

        // Use this for initialization
        void Start()
        {
            Value = Constants.DefaultValues.RefugeeValue;
            _lifetime = Constants.DefaultValues.RefugeeLifespan;
            _gameMaster = FindObjectOfType<GameMaster>();
            Drowning = false;            
        }

        // Update is called once per frame
        void Update()
        {

            //Only update the living
            if (Drowning)
            {
                //Decrease liftime
                _lifetime -= Time.deltaTime;

                //Are we dead yet?
                if (_lifetime <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Tells the refugee that he has been dumped of the boat, you heartless bastard.
        /// </summary>
        public void Dump(Vector3 position)
        {
            transform.position = position;
            transform.FindChild("RefugeeBody").gameObject.SetActive(true);
            var audioHandler = _gameMaster.GetComponent<AudioHandler>();
            audioHandler.Play(audioHandler.DumpSound);

            Drowning = true;
        }

        /// <summary>
        /// Save a refugee from the sea by inviting on board. What a nice person you are.
        /// </summary>
        public void PickUp()
        {
            transform.FindChild("RefugeeBody").gameObject.SetActive(false);
            transform.position = Constants.DefaultValues.AwayPosition;
            Drowning = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Player)
                if (other.gameObject.GetComponent<RefugeeContainer>().TryAddRefugee(this))
                    PickUp();

        }
    }
}
