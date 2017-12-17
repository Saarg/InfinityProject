using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : EnemyState {

	float interp;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.red;
		animator.SetBool ("PlayerIsDetected", true);
		animator.SetBool ("PlayerIsDead", false);
		animator.SetBool ("PlayerIsLost", false);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen () || enemy.IsUnderAttack() || enemy.PlayerIsHeard())
			Chase ();
		else if (enemy.target != null) {
			enemy.lastPlayerKnownLocation = enemy.target.transform.position;
			animator.SetBool ("PlayerIsDetected", false);
		} else {
			animator.SetBool ("PlayerIsDead", true);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}


	/** Chase() : void
	 * Look at the player than move towards it if too far or attack if enemy in attack range
	 */
	private void Chase()
	{
		Vector3 direction = enemy.target.position - enemy.transform.position;

		if(direction != Vector3.zero){
			enemy.transform.rotation = Quaternion.LookRotation(direction);
		}
			
		if (direction.magnitude > enemy.specs.attackRange*.5f) {
			//too far
			enemy.Move (direction);
		} 

		if (direction.magnitude <= enemy.specs.attackRange){ 
			//close enough to attack
			enemy.Shoot();
		}
	}

	/** LookAtPlayer() : void 	[legacy]
	 * rotate enemy towards player
	 */
	private void LookAtPlayer()
	{
		Quaternion start = enemy.transform.rotation;
		Quaternion finish = enemy.transform.rotation;

		// cancel height to avoid loking strainght up if player is on my head
		Vector3 lookat = enemy.target.transform.position - enemy.transform.position;
		lookat.y = 0;

		finish.SetLookRotation (lookat);

		enemy.transform.rotation = Quaternion.Lerp (start, finish, interp);

		interp += enemy.specs.chasingTurningSpeed * Time.deltaTime;
	}
}
