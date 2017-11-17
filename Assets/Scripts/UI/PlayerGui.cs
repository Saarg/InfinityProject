using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGui : MonoBehaviour {

	public PlayerController player;

	public Image gunLogo;

	public Slider healthBar;
	public Slider ammoBar;
	public Slider staminaBar;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		healthBar.maxValue = player.maxLife;
		staminaBar.maxValue = player.staminaMax;
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = player.life;
		staminaBar.value = player.stamina;

		if (player.gun != null) {
			ammoBar.gameObject.SetActive (true);
			gunLogo.gameObject.SetActive (true);

			ammoBar.maxValue = player.gun.GetClipSize ();
			ammoBar.value = player.gun.ammos;
			gunLogo.sprite = player.gun.GetLogo();
		} else {
			ammoBar.gameObject.SetActive (false);
			gunLogo.gameObject.SetActive (false);
		}
	}
}
