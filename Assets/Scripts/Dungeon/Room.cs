using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon {
	public class Room : MonoBehaviour {

		static Color[] waveColors = {Color.red, Color.green, Color.blue, Color.yellow, Color.cyan};

		[Header("Waves config")]
		[SerializeField]
		protected Wave[] _waves;
		protected int _curWave = -1;

		[SerializeField]
		protected GameObject _reward;

		[Range(0f, 1f)]
		[SerializeField]
		protected float _rewardChance = 0.2f;

		[Header("Room Events")]
		public UnityEvent OnStart;
		public UnityEvent OnCompleted;
		public UnityEvent OnPlayerEnter;

		protected PlayerController _player;
		public PlayerController player { get { return _player; } }

		void Start () {
			if (_reward != null) {
				_reward.SetActive (false);
			}

			OnStart.Invoke ();

			if (_waves.Length == 0) {
				OnCompleted.Invoke ();

				SpawnReward ();
				Destroy (this);
			}
		}

		IEnumerator ManageWaves () {
			_waves [++_curWave].StartWave (transform.position);

			while (true) {
				if (_waves [_curWave].Completed ()) {
					_curWave++;

					if (_curWave < _waves.Length - Random.Range(0, 1)) {
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

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player" && _curWave == -1) {
				_player = other.GetComponent<PlayerController>();

				StartCoroutine (ManageWaves());

				OnPlayerEnter.Invoke ();
			}
		}

		void SpawnReward() {
			if (_reward != null && Random.Range (0f, 1f) < _rewardChance) {
				_reward.SetActive (true);
			}
		}

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
