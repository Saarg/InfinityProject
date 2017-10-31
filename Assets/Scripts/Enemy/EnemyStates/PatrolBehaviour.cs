using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : EnemyState {

	private float elapsedTime;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.green;
		animator.SetBool ("PlayerIsSeen", false);

		elapsedTime = 0;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		elapsedTime += Time.deltaTime;

		if (enemy.PlayerIsSeen ())
			animator.SetBool ("PlayerIsSeen", true);
		else {
			Patrol ();
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void Patrol()
	{
//		Debug.Log ("patrolling");
	}
}
