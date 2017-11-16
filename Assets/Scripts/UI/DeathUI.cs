using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour {

	public Living player;

	public Canvas canvas;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Living> ();

		canvas.gameObject.SetActive (false);

		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if ((player == null || player.life <= 0) && Time.timeScale > 0) {
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
