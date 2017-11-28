using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehaviour : EnemyState {

	private float searchTime;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.yellow;
		searchTime = 0;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen () || enemy.PlayerIsHeard() || (enemy.IsHit && enemy.player != null))
			animator.SetBool ("PlayerIsDetected", true);
		else
			SearchForPlayer(animator);
		}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void SearchForPlayer(Animator animator)
	{
		enemy.transform.Rotate (new Vector3(0, 360 * Time.deltaTime / enemy.specs.alertDuration, 0));

		searchTime += Time.deltaTime;
		if (searchTime > enemy.specs.alertDuration)
			animator.SetBool ("PlayerIsLost", true);
	}
}