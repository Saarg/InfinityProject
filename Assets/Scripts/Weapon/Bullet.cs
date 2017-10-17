using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	public class Bullet : MonoBehaviour {

		[SerializeField]
		private BulletSpec _specs;

		private Rigidbody _rb;

		void Start () {
			_rb = GetComponent<Rigidbody> ();

			if (_rb == null) {
				Debug.LogWarning ("No rigidbody found on bullet, destroying bullet script");
				Destroy (this);
			}

			_rb.velocity = transform.forward * _specs.velocity;
			Destroy (gameObject, _specs.lifespan);
		}

		void OnCollisionEnter(Collision collision) {
			Destroy (gameObject);
		}
	}
}