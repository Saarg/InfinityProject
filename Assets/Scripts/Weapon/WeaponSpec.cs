using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun", order = 1)]
	public class WeaponSpec : ScriptableObject {
		public int clipSize;
		public int clips;

		public float firerate;
		public float accuracy;
        public float dispersion;

		public float reloadTime;

        public GameObject ammoPrefab;

		public Sprite logo;

		public void Fire(Transform t) {
			Transform canon = t.Find ("Canon");
            Instantiate(ammoPrefab, canon.position, canon.rotation);
        }
	}
}
