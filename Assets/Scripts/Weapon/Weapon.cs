using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	public class Weapon : MonoBehaviour {

		[SerializeField]
		private WeaponSpec _specs;
        private float nextFire = 0.0F;

        // Use this for initialization
        void Start () {
			
		}

		public void Fire() {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + this.GetFireRate();
                _specs.Fire(transform);
            }
            Debug.Log ("pew !");
		}

		public float GetFireRate() {
			return _specs.firerate;
		}
	}
}