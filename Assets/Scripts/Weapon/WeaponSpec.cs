using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun", order = 1)]
	public class WeaponSpec : ScriptableObject {
		public int clipSize;
		public int clips;

		public float firerate;

		public float reloadTime;

        public GameObject ammoPrefab;

		public Sprite logo;

		public virtual void Fire(Transform t, Transform owner) {
			Transform canon = t.Find ("Canon");
            GameObject go = Instantiate(ammoPrefab, canon.position, canon.rotation);
            Bullet b = (Bullet)go.GetComponent(typeof(Bullet));
            b.owner = owner;
        }
	}
}
