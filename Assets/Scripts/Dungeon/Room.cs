using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon {
	public class Room : MonoBehaviour {

		static Color[] waveColors = {Color.red, Color.green, Color.blue, Color.yellow, Color.cyan};

		[SerializeField]
		protected Wave[] _waves;

		protected int _curWave = -1;

		public UnityEvent OnStart;
		public UnityEvent OnCompleted;
		public UnityEvent OnPlayerEnter;

		void Start () {
			OnStart.Invoke ();
		}

		IEnumerator ManageWaves () {
			_waves [++_curWave].StartWave (transform.position);

			while (true) {
				if (_waves [_curWave].Completed ()) {
					_curWave++;

					if (_curWave < _waves.Length) {
						Debug.Log ("Spawning wave " + _curWave);

						_waves [_curWave].StartWave (transform.position);
					} else {
						Debug.Log ("Room Cleared");

						OnCompleted.Invoke ();

						break;
					}
				}

				yield return new WaitForSeconds (0.2f);
			}
		}

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player" && _curWave == -1) {
				StartCoroutine (ManageWaves());

				OnPlayerEnter.Invoke ();
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
