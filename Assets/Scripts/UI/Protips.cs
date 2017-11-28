using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protips : MonoBehaviour {

	public Text display;
	public string[] tips;

	void Awake() {
		GetComponent<Canvas> ().enabled = true;
	}

	// Use this for initialization
	void Start () {
		display.text = tips[Random.Range(0, tips.Length-1)];
	}
}
