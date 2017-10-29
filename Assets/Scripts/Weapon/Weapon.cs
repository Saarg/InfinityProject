using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	public class Weapon : MonoBehaviour {

		[SerializeField]
		private WeaponSpec _specs;

		// Use this for initialization
		void Start () {
			
		}

		public void Fire() {
			_specs.Fire (transform);
			Debug.Log ("pew !");
		}
	}
}