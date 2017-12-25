using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRoom : MonoBehaviour {

	[SerializeField]
	private GameObject[] _bosses;
	[SerializeField]
	private GameObject _spawnedBoss;

	[Header("Room Events")]
	public UnityEvent OnStart;
	public UnityEvent OnCompleted;
	public UnityEvent OnPlayerEnter;

	private bool started;

	// Use this for initialization
	void Start () {
		OnStart.Invoke ();
	}
	
	// Update is called once per frame
	void Update () {
		if (started && _spawnedBoss == null) {
			OnCompleted.Invoke ();
		}
	}

	/*
	 * Detection of player
	 */
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && _spawnedBoss == null) {
			_spawnedBoss = Instantiate (_bosses[Random.Range(0, _bosses.Length)], transform.position, transform.rotation);

			started = true;

			OnPlayerEnter.Invoke ();

			Camera.main.GetComponent<CameraController> ().SetTarget (transform);
		}
	}
}
