using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : EnemyState {

	private float elapsedTime;
	public Vector3 target;

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
			if (enemy.WallIsSeen ()) {
				elapsedTime = 1; //force direction change
			}
			Patrol ();
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void Patrol()
	{
//		Debug.Log ("patrolling");
		if (elapsedTime >= 1f) {
			//modify rotation
			Vector3 rot = enemy.transform.rotation.eulerAngles;
			float jitter = Random.Range (enemy.specs.wanderingRange * -1, enemy.specs.wanderingRange);
			rot.y += jitter;
			enemy.transform.Rotate (rot);
			elapsedTime = 0;
		}

		//move forward
		enemy.Move (enemy.transform.forward.normalized * enemy.specs.patrollingSpeed);
	}
}
