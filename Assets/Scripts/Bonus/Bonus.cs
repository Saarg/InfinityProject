using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

	public AnimationCurve yMovement;

	void Update() {
		transform.Rotate (0, 40 * Time.deltaTime, 0);

		transform.Translate(Vector3.up * Time.deltaTime * yMovement.Evaluate((Time.realtimeSinceStartup % 10) / 10));
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" && col.GetComponent<Living> () != null) {
			Apply(col.GetComponent<Living> ());
		}
	}

	protected virtual void Apply (Living entity) {
		Debug.Log ("Bonus not implemented!");
	}
}
