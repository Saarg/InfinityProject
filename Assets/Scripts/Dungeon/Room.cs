using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon {

	/*
	 * Class used to display reward array in editor
	 */
	[System.Serializable]
	public struct RewardEditor {
		public GameObject gameobject;

		[Range(0f, 1f)]
		public float rewardChance;
	}

	/*
	 * Room script: spawns, rewards and events
	 */
	public class Room : MonoBehaviour {

		static Color[] waveColors = {Color.red, Color.green, Color.blue, Color.yellow, Color.cyan};

		[Header("Waves config")]

		[SerializeField]
		protected int _minWaves = 0;
		[SerializeField]
		protected int _maxWaves = 10;

		[SerializeField]
		protected Wave[] _waves;
		protected int _curWave = -1;

		[Header("Rewards config")]
		[SerializeField]
		protected RewardEditor[] _rewards;

		[Header("Room Events")]
		public UnityEvent OnStart;
		public UnityEvent OnCompleted;
		public UnityEvent OnPlayerEnter;

		protected PlayerController _player;
		public PlayerController player { get { return _player; } }

		/*
		 * Init room according to difficulty
		 * Spawn reward if no wave to spawn
		 */
		void Start () {
			_maxWaves = Mathf.Clamp(_maxWaves, 0, _waves.Length);
			if ((int)GameMode.difficulty < _maxWaves)
				_minWaves = Mathf.Clamp (_minWaves, (int)GameMode.difficulty, _maxWaves);
			else {
				_minWaves = _maxWaves;
			}

			_maxWaves = Mathf.Clamp(_maxWaves, _minWaves, _waves.Length);

			foreach (RewardEditor r in _rewards) {
				r.gameobject.SetActive (false);
			}

			OnStart.Invoke ();

			if (_waves.Length == 0) {
				OnCompleted.Invoke ();

				SpawnReward ();
				Destroy (this);
			}
		}

		/*
		 * Room main loop
		 */
		IEnumerator ManageWaves () {
			int nbWaves = Random.Range (_minWaves, _maxWaves);

			_waves [++_curWave].StartWave (transform.position);

			while (true) {
				if (_waves [_curWave].Completed ()) {
					_curWave++;

					if (_curWave < nbWaves) {
						_waves [_curWave].StartWave (transform.position);
					} else {
						Debug.Log ("Room Cleared");

						SpawnReward ();

						OnCompleted.Invoke ();

						break;
					}
				}

				yield return new WaitForSeconds (0.2f);
			}
		}

		/*
		 * Detection of player
		 */
		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player" && _curWave == -1) {
				_player = other.GetComponent<PlayerController>();

				StartCoroutine (ManageWaves());

				OnPlayerEnter.Invoke ();
			}
		}

		/*
		 * Randomly spawn a reward from list
		 */
		void SpawnReward() {
			foreach (RewardEditor r in _rewards) {
				if (Random.Range (0f, 1f) < r.rewardChance) {
					r.gameobject.SetActive (true);
					break;
				}
			}
		}

		/*
		 * Draw waves spawn in editor
		 */
		void OnDrawGizmos() {
			for (int i = 0; i < _waves.Length; i++) {
				Gizmos.color = waveColors[i % _waves.Length];

				foreach (ISpawns s in _waves[i].spawns) {
					Gizmos.DrawSphere(transform.position + s.p, 0.1f);
				}
			}
		}
	}
}
