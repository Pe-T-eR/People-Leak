using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public string[] playerNames;
	public Color[] playerColors;
	public GameObject[] boatBodies;
	public Boat[] playerBoats;
	public Text[] playerScores;
	public Text[] playerRefugeeCounters;
	public int goalScore;
	public Text winningText;

	protected bool gameover = false;

	// Use this for initialization
	void Start ()
	{

		ResetGame();

		for (var i = 0; i < boatBodies.Length; i++) {
			if (playerColors.Length <= i) {
				break;
			}
			var boatRenderers = boatBodies[i].gameObject.GetComponentsInChildren<Renderer>();
			//var boatRenderers = playerBoats[i].GetComponents<Renderer>();
			foreach (var renderer in boatRenderers) {
				renderer.material.color = playerColors[i];
			}

			if (playerScores.Length > i && playerScores[i] != null) {
				playerScores[i].color = playerColors[i];
			}
			if (playerRefugeeCounters.Length > 1 && playerRefugeeCounters[i] != null) {
				playerRefugeeCounters[i].color = playerColors[i];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        var winningPlayers = new List<string>();

		for (var i = 0; i < playerBoats.Length; i++) {
			if (playerBoats[i] == null) {
				continue;
			}
			if (!gameover && playerScores.Length > i) {
				var score = playerBoats[i].Score;
				if (score > goalScore) {
					winningPlayers.Clear();
					goalScore = score;
				}
				if (score >= goalScore) {
					gameover = true;
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
			winningText.text = winnerText;
		    StartCoroutine(RestartScene());
		}
	}

	public void ResetGame() {
		winningText.enabled = false;
	}

    private static IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    } 
}
