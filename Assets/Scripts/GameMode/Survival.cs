using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Survival : GameMode {

	private float _start;
	private float _finalTime;

	public Text scoreboard;

	public float[] highscores;

	// Use this for initialization
	protected override void Start () {
		_start = Time.realtimeSinceStartup;

		base.Start ();
		allowCoop = false;

		if (File.Exists (Application.persistentDataPath + "/survival.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/survival.dat", FileMode.Open);
			highscores = (float[])bf.Deserialize (file);

			file.Close ();
		} else {
			highscores = new float[]{ 0, 0, 0, 0, 0 };
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (_player1 == null || _player1.GetComponent<PlayerController> ().life <= 0) {
			_finalTime = Time.realtimeSinceStartup - _start;

			scoreboard.text = "";

			for (int i = 0; i < 5; i++) {
				if (highscores [i] < _finalTime) {
					for (int j = highscores.Length - 1 ; j > i; j--) {
						highscores [j] = highscores [j - 1];
					}
					
					highscores [i] = _finalTime;

					break;
				}
			}

			for (int i = 0 ; i < 5 ; i++) {
				int hours = (int)((highscores [i] / 60) / 60);
				int minutes = (int)((highscores [i] / 60) % 60);
				int seconds = (int)(highscores [i] % 60);

				scoreboard.text += (i + 1) + ". " + hours + ":" + minutes + ":" + seconds + '\n';
			}

			Destroy (this);
		}
	}

	void OnDestroy() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/survival.dat");

		bf.Serialize(file, highscores);
		file.Close();
	}
}
