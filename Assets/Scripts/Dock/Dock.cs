﻿using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Dock
{
    public class Dock : MonoBehaviour
    {
        protected List<Boat> DockedBoats;

        protected void Start()
        {
            DockedBoats = new List<Boat>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedBoats.Add(other.GetComponent<Boat>());
        }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == Constants.Tags.Player)
                DockedBoats.Remove(other.GetComponent<Boat>());
        }
    }
}
