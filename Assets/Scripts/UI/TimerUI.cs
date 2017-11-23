using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

	public Text display;
	private float start;

	// Use this for initialization
	void Start () {
		start = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		float time = Time.realtimeSinceStartup - start;

		int hours = (int)((time / 60) / 60);
		int minutes = (int)((time / 60) % 60);
		int seconds = (int)(time % 60);

		display.text = "";

		if (hours != 0) {
			display.text += hours + ":";
		} else if (minutes != 0) {
			display.text += minutes + ":";
		}
		display.text += seconds;
	}
}
