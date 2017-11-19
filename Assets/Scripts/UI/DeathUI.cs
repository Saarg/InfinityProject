using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour {

	public GameObject[] players;
	public int minPlayerAlive = 0;

	public Canvas canvas;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");

		canvas.gameObject.SetActive (false);

		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		int livingPlayers = 0;
		foreach (GameObject go in players) {
			if (go != null) {
				livingPlayers++;
			}
		}


		if (livingPlayers <= minPlayerAlive && Time.timeScale > 0) {
			Time.timeScale = 0;

			canvas.gameObject.SetActive (true);
		}
	}

	public void LoadScene(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void ReloadScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
