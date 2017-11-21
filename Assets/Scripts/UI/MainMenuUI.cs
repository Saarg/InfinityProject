using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public void LoadScene(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void Quit() {
		Application.Quit ();
	}

    public void NewGame(string name)
    {
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/playerInfo.dat");
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
