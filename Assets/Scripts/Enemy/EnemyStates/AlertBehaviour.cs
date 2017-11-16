using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehaviour : EnemyState {

	private Quaternion startRotation;
	private float searchTime;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.yellow;
		searchTime = 0;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen () || enemy.PlayerIsHeard())
			animator.SetBool ("PlayerIsDetected", true);
		else
			SearchForPlayer(animator);
		}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void SearchForPlayer(Animator animator)
	{
		Vector3 diff = enemy.lastPlayerKnownLocation - enemy.transform.position;
		if (diff.magnitude < 2f) {
			if (startRotation == null) {
				startRotation = enemy.transform.rotation;
			}

			enemy.transform.Rotate (new Vector3(0, 360 * Time.deltaTime / enemy.specs.alertDuration, 0));

			searchTime += Time.deltaTime;
			if (searchTime > enemy.specs.alertDuration)
				animator.SetBool ("PlayerIsLost", true);
		} else {
			enemy.Move (diff.normalized * enemy.specs.alertSpeed);
		}
	}
}


/**
 * GameObject go;
 * float angle = 360f;
 * float time = 1.0f;
 * 
 * Vector3 axis = vector3.up; //rotation axis
 * 
 * 
*/