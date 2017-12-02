using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard, Insane }

public class GameMode : MonoBehaviour {

	static protected Difficulty _difficulty;
	static public Difficulty difficulty { get { return _difficulty; } }

	protected GameObject _player1;
	protected GameObject _player2;
	protected GameObject _player3;
	protected GameObject _player4;

	public Difficulty startDifficulty = Difficulty.Normal;
	public bool allowCoop;
	public bool spawnOnPlayer = true;

	// Use this for initialization
	protected virtual void Start () {
		_difficulty = startDifficulty;

		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
			if (p.GetComponent<PlayerController> ().player == PlayerNumber.Player1) {
				_player1 = p;
				_player1.transform.parent.gameObject.SetActive (true);
			} else if (p.GetComponent<PlayerController> ().player == PlayerNumber.Player2) {
				_player2 = p;
				_player2.transform.parent.gameObject.SetActive (false);
			} else if (p.GetComponent<PlayerController> ().player == PlayerNumber.Player3) {
				_player3 = p;
				_player3.transform.parent.gameObject.SetActive (false);
			} else if (p.GetComponent<PlayerController> ().player == PlayerNumber.Player4) {
				_player4 = p;
				_player4.transform.parent.gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (allowCoop) {
			if (MultiOSControls.GetValue ("Join", PlayerNumber.Player2) != 0 && _player2 != null && !_player2.transform.parent.gameObject.activeSelf) {
				_player2.transform.parent.gameObject.SetActive (true);

				if (spawnOnPlayer)
					_player2.transform.position = _player1.transform.position + Vector3.up;

				_difficulty++;
			}

			if (MultiOSControls.GetValue ("Join", PlayerNumber.Player3) != 0 && _player3 != null && !_player3.transform.parent.gameObject.activeSelf) {
				_player3.transform.parent.gameObject.SetActive (true);

				if (spawnOnPlayer)
					_player3.transform.position = _player1.transform.position + Vector3.up;

				_difficulty++;
			}

			if (MultiOSControls.GetValue ("Join", PlayerNumber.Player4) != 0 && _player4 != null && !_player4.transform.parent.gameObject.activeSelf) {
				_player4.transform.parent.gameObject.SetActive (true);

				if (spawnOnPlayer)
					_player4.transform.position = _player1.transform.position + Vector3.up;

				_difficulty++;
			}
		}
	}
}
