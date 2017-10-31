using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {

	[Serializable]
	public class ISpawns {
		public Vector3 p;
		public GameObject go;
	}

	[CreateAssetMenu(fileName = "Wave", menuName = "Dungeon/Wave", order = 1)]
	public class Wave : ScriptableObject {

		public ISpawns[] spawns;
		public float nextWaveCompletion;
		public GameObject[] rewards;
		public bool completed;

		private GameObject[] _spawned;

		public void StartWave(Vector3 position) {
			_spawned = new GameObject[spawns.Length];

			for (int i = 0; i < spawns.Length; i++) {
				_spawned[i] = Instantiate (spawns[i].go, position + spawns[i].p, Quaternion.identity);
			}
		}

		public bool Completed() {
			bool done = true;

			for (int i = 0; i < spawns.Length; i++) {
					done = _spawned[i] == null ? done : false;
			}

			return done;
		}
	}
}
