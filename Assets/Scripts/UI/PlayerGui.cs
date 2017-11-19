using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGui : MonoBehaviour {

	public PlayerNumber player;
	private PlayerController _player;

	public Image gunLogo;

	public Slider healthBar;
	public Slider ammoBar;
	public Slider staminaBar;

	// Use this for initialization
	void Start () {
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
			PlayerController pc = p.GetComponent<PlayerController> ();

			if (pc != null && pc.player == player) {
				_player = pc;
			}
		}

		if (_player == null) {
			Debug.LogError ("No player found for " +  name);
			Destroy (gameObject);
			return;
		}

		healthBar.maxValue = _player.maxLife;
		staminaBar.maxValue = _player.staminaMax;
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = _player.life;
		staminaBar.value = _player.stamina;

		if (_player.gun != null) {
			ammoBar.gameObject.SetActive (true);
			gunLogo.gameObject.SetActive (true);

			ammoBar.maxValue = _player.gun.GetClipSize ();
			ammoBar.value = _player.gun.ammos;
			gunLogo.sprite = _player.gun.GetLogo();
		} else {
			ammoBar.gameObject.SetActive (false);
			gunLogo.gameObject.SetActive (false);
		}
	}
}
