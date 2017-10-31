using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehaviour : EnemyState {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.yellow;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen ())
			animator.SetBool ("PlayerIsSeen", true);
		else
			SearchForPlayer ();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void SearchForPlayer()
	{
		//searching for player
	}
}
