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
		direction.y = 0;

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
}
