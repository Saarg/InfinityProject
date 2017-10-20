using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapons;

public class Living : MonoBehaviour {

	[SerializeField]
	protected float _life = 100.0F;
	[SerializeField]
	protected float _maxLife = 100.0F;
	[SerializeField]
	protected float _speed = 6.0F;

	protected CharacterController _controller;
	[SerializeField]
	protected float _gravity = 20.0F;
	protected Vector3 _moveDirection = Vector3.zero;

	[SerializeField]
	protected Weapon _gun;

	protected virtual void Start() {
		_controller = GetComponent<CharacterController>();
	}

	protected virtual void Update() {
		
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.transform.tag);
		if (collision.transform.tag == "Projectile") {
			_life -= collision.transform.GetComponent<Bullet> ().GetDamages ();

			if (_life <= 0)
				Destroy (this.gameObject);
		}
	}
}
