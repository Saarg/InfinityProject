using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	[SerializeField] private float _gravity;
	[SerializeField] private float _strength;

	void Start () {
		_gravity = 0.4f;
	}
	
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y - _gravity, transform.position.z);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.collider.gameObject.name);
		if (collision.gameObject.CompareTag ("Player"))
			collision.gameObject.SendMessage ("ApplyDamage", _strength, SendMessageOptions.DontRequireReceiver);

		Destroy (gameObject);
	}
}
