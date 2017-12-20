using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnTrigger : MonoBehaviour {

	[SerializeField]
	private float force = 0.5f;
	[SerializeField]
	private float damages = 30f;

	private float lastHit = 0f;
	[SerializeField]
	private float hitCooldown = 0.1f;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (Time.realtimeSinceStartup - lastHit > hitCooldown) {
				other.GetComponent<CharacterController> ().Move((transform.forward + transform.up) * force);

				other.SendMessage ("ApplyDamage", damages, SendMessageOptions.DontRequireReceiver);

				lastHit = Time.realtimeSinceStartup;
			}
		}
	}
}
