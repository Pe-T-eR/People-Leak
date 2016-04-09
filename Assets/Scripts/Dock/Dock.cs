using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class Dock : MonoBehaviour
    {
        protected List<GameObject> DockedShips;

        protected void Start()
        {

            DockedShips = new List<GameObject>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedShips.Add(other.gameObject);
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedShips.Remove(other.gameObject);
        }
    }
}
