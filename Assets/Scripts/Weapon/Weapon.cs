using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	
	[RequireComponent(typeof(AudioSource))]

	public class Weapon : MonoBehaviour {

		[SerializeField]
		private WeaponSpec _specs;
        private float nextFire = 0.0F;

		public int ammos { get; internal set; }
		public int clips { get; internal set; }

		public bool canShoot { get; internal set; }

		public AudioClip fireSound;
		public AudioClip reloadSound;
		private AudioSource _source;

        // Use this for initialization
        void Start () {
			ammos = _specs.clipSize;
			clips = _specs.clips;

			canShoot = true;

			_source = GetComponent<AudioSource> ();
		}

		public void Fire() {
			if (!canShoot)
				return;

			if (ammos <= 0) {
				StartReload ();
			}
			else if (Time.time > nextFire)
            {
                nextFire = Time.time + this.GetFireRate();

				_source.clip = fireSound;
				_source.Play ();

                _specs.Fire(transform);

				ammos--;
            }
		}

		public void StartReload() { 
			StartCoroutine (Reload ());
		}

		IEnumerator Reload() {
			canShoot = false;
			float startTime = Time.realtimeSinceStartup;

			_source.clip = reloadSound;
			_source.Play ();

			while (ammos < _specs.clipSize) {
				ammos++;
				yield return new WaitForSeconds (_specs.reloadTime / _specs.clipSize);
			}

			clips--;
			canShoot = true;
		}

		public float GetFireRate() {
			return _specs.firerate;
		}

		public float GetMaxAmmo() {
			return _specs.clips;
		}

		public float GetClipSize() {
			return _specs.clipSize;
		}
	}
}