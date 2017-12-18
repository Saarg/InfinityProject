using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour {

	public GameObject[] players;
	public int minPlayerAlive = 0;
    public GameControl gc;
    public GameObject Chart;

    public Canvas canvas;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");

		canvas.gameObject.SetActive (false);

		Time.timeScale = 1;

        AutoLevelUp();
    }
	
	// Update is called once per frame
	void Update () {
		if (MultiOSControls.GetValue("Join", PlayerNumber.All) != 0)
			players = GameObject.FindGameObjectsWithTag ("Player");


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
		if (gc != null)
        	gc.Save();
		
        SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void ReloadScene() {
		if (gc != null)
        	gc.Save();
		
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    protected void AutoLevelUp()
    {
        StatManager sm = StatManager.Instance;
        sm.LevelUp();
    }
}
