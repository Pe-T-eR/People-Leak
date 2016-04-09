using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public string[] playerNames;
	public Boat[] playerBoats;
	public Text[] playerScores;
	public Text[] playerRefugeeCounters;
	public int goalScore;
	public Text winningText;

    public GameObject GameMusic;

	// Use this for initialization
	void Start ()
	{
	    var music = GameObject.FindGameObjectWithTag(Constants.Tags.Music);
	    if (music == null)
	        DontDestroyOnLoad(Instantiate(GameMusic).transform);

		ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
		var winningPlayers = new List<string>();

		for (var i = 0; i < playerBoats.Length; i++) {
			if (playerBoats[i] == null) {
				continue;
			}
			if (playerScores.Length > i) {
				var score = playerBoats[i].Score;
				if (score > goalScore) {
					winningPlayers.Clear();
					goalScore = score;
				}
				if (score >= goalScore) {
					var playerName = playerNames[i];
					if (String.IsNullOrEmpty(playerName)) {
						playerName = String.Format("Unknown player {0}", i);
					}
					winningPlayers.Add(playerName);
				}
				playerScores[i].text = String.Format("${0}", score);
			}

			if (playerRefugeeCounters.Length > i) {
				playerRefugeeCounters[i].text = String.Format("Refugees: {0}", playerBoats[i].RefugeeContainer.GetCount());
			}
		}

		if (winningPlayers.Count > 0) {
			// Game over!
			winningText.enabled = true;
			var winnerText = "Game over!\n";
			winnerText += winningPlayers.Count == 1 ? "Winner:" : "Winners:";
			foreach (var winningPlayer in winningPlayers) {
				winnerText += "\n" + winningPlayer;
			}
		    StartCoroutine(RestartScene());
		}
	}

	public void ResetGame() {
		winningText.enabled = false;
	}

    private static IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}
