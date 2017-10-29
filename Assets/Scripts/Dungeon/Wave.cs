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

		public void StartWave() {
			foreach (ISpawns s in spawns) {
//				Instantiate (s.go, s.p, Quaternion.identity);
			}
		}
	}
}