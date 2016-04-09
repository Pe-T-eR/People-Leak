using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class Dock : MonoBehaviour
    {
        protected List<RefugeeContainer> DockedShips;

        protected void Start()
        {
            DockedShips = new List<RefugeeContainer>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedShips.Add(other.GetComponent<RefugeeContainer>());
        }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedShips.Remove(other.GetComponent<RefugeeContainer>());
        }
    }
}
