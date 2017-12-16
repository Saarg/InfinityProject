using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class handling camera to follow the average position of a player list with an offset
 */
public class CameraController : MonoBehaviour {
	
	public GameObject[] _players;
	public GameObject _player;
	private Vector3 _offset;

	void Start () 
	{
		if (_player == null)
			_players = GameObject.FindGameObjectsWithTag ("Player");
		else 
			_players = new GameObject[] {_player};

		Vector3 avg = Vector3.zero;
		foreach (GameObject p in _players) {
			avg += p.transform.position;
		}
		avg /= _players.Length;

		_offset = transform.position - avg;
	}

	void Update() {
		if (_player == null && MultiOSControls.GetValue("Join", PlayerNumber.All) != 0)
			_players = GameObject.FindGameObjectsWithTag ("Player");
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		int i = 0;
		Vector3 avg = Vector3.zero;
		foreach (GameObject p in _players) {
			if (p != null) {
				avg += p.transform.position;
				i++;
			}
		}

		if (i == 0) { return; }

		avg /= i;

		transform.position = Vector3.Lerp(transform.position, avg + _offset, 3*Time.deltaTime);
	}
}
