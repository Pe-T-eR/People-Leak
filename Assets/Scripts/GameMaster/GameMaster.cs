using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public List<Boat> playerBoats;
	public List<Text> playerScores;
	public List<Text> playerRefugeeCounters;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (var i = 0; i < playerBoats.Count; i++) {
			if (playerScores.Count > i) {
				playerScores[i].text = String.Format("${0}", playerBoats[i].Score);
			}
			if (playerRefugeeCounters.Count > i) {
				playerRefugeeCounters[i].text = String.Format("Refugees: {0}", playerBoats[i].RefugeeContainer.GetCount());
			}
		}
	}
}
