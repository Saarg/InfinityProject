﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGui : MonoBehaviour {

	public Living player;

	public Slider healthBar;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = player.maxLife;
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = player.life;
	}
}
