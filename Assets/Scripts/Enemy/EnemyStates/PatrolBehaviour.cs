using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : EnemyState {

	private float elapsedTime;
	public Vector3 target;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//init
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.green;
		animator.SetBool ("PlayerIsDetected", false);
		elapsedTime = 0;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		elapsedTime += Time.deltaTime;

		//if the player is detected in any way, go to chase behavior
		if (enemy.PlayerIsSeen () || enemy.PlayerIsHeard() || enemy.IsUnderAttack())
			animator.SetBool ("PlayerIsDetected", true);
		else {
			//wander around avoiding walls
			if (enemy.WallIsSeen ()) {
				AvoidWall();
			}
			Patrol ();
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}


	/** Patrol() : void
	 * regularly pick another direction then welks straight to it
	 */
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


	/** AvoidWall() : void
	 * check is a wall is in range of sight and avoid it if necessary
	 */
	private void AvoidWall()
	{
		float angle = 20f;

		if (enemy.hitWallRight)
			angle *= -1;

		enemy.transform.RotateAround (enemy.transform.position, Vector3.up, angle);
	}
}
