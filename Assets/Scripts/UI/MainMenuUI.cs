using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public GameObject cont;

	void Start() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			cont.SetActive (true);
		} else {
			cont.SetActive (false);
		}
	}

	public void LoadScene(string name) {
		Time.timeScale = 1f;

		StartCoroutine(LoadSceneAfterDelay(name));
	}

	IEnumerator LoadSceneAfterDelay(string name) {
		yield return new WaitForSeconds(0.3f);
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
	}

	public void Quit() {
		Time.timeScale = 1f;

		StartCoroutine(QuitAfterDelay());
	}

	IEnumerator QuitAfterDelay() {
		yield return new WaitForSeconds(0.3f);
		Application.Quit ();
	}

    public void NewGame(string name)
    {
		Time.timeScale = 1f;

		File.Delete (Application.persistentDataPath + "/playerInfo.dat");
		StartCoroutine(LoadSceneAfterDelay(name));
    }
}
