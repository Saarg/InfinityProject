using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCountDisplay : MonoBehaviour {

	[Header("default color")]
	[SerializeField]
	private ColorBlock def;
	[Header("selected color")]
	[SerializeField]
	private ColorBlock selected;

	[SerializeField]
	private GameObject player1;
	[SerializeField]
	private GameObject player2;
	[SerializeField]
	private GameObject player3;
	[SerializeField]
	private GameObject player4;

	[SerializeField]
	private Button player1Button;
	[SerializeField]
	private Button player2Button;
	[SerializeField]
	private Button player3Button;
	[SerializeField]
	private Button player4Button;

	void Update () {
		if (player1.activeSelf && player2.activeSelf && player3.activeSelf && player4.activeSelf) {
			player1Button.colors = def;
			player2Button.colors = def;
			player3Button.colors = def;
			player4Button.colors = selected;
		} else if (player1.activeSelf && player2.activeSelf && player3.activeSelf && !player4.activeSelf) {
			player1Button.colors = def;
			player2Button.colors = def;
			player3Button.colors = selected;
			player4Button.colors = def;
		} else if (player1.activeSelf && player2.activeSelf && !player3.activeSelf && !player4.activeSelf) {
			player1Button.colors = def;
			player2Button.colors = selected;
			player3Button.colors = def;
			player4Button.colors = def;
		} else if (player1.activeSelf && !player2.activeSelf && !player3.activeSelf && !player4.activeSelf) {
			player1Button.colors = selected;
			player2Button.colors = def;
			player3Button.colors = def;
			player4Button.colors = def;
		}
 	}

	public void LockControls() {
		MultiOSControls.instance.locked = true;
	}

	public void UnLockControls() {
		MultiOSControls.instance.locked = false;
	}
}
