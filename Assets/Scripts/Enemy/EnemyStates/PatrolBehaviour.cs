using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : EnemyState {

	private float elapsedTime;
	public Vector3 target;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.green;
		animator.SetBool ("PlayerIsDetected", false);

		elapsedTime = 0;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		elapsedTime += Time.deltaTime;

		if (enemy.PlayerIsSeen () || enemy.PlayerIsHeard() || enemy.IsHit)
			animator.SetBool ("PlayerIsDetected", true);
		else {
			if (enemy.WallIsSeen ()) {
				AvoidWall();
			}
			Patrol ();
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void Patrol()
	{
		if (elapsedTime >= 1f) {
			//modify rotation
			float jitter = Random.Range (enemy.specs.wanderingRange * -1, enemy.specs.wanderingRange);
			enemy.transform.Rotate(enemy.transform.up * jitter);

			elapsedTime = 0;
		}

		//move forward
		enemy.Move (enemy.transform.forward.normalized * enemy.specs.patrollingSpeed);
	}

	private void AvoidWall()
	{
		float angle = 20f;

		if (enemy.hitWallRight)
			angle *= -1;

		enemy.transform.RotateAround (enemy.transform.position, Vector3.up, angle);
	}
}
