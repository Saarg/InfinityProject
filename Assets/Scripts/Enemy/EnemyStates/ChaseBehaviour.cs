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
		if (enemy.PlayerIsSeen ())
			Chase ();
		else if (enemy.PlayerIsHeard ())
			LookAtPlayer ();
		else
			animator.SetBool ("PlayerIsDetected", false);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void Chase()
	{
		Vector3 direction = enemy.player.position - enemy.transform.position;

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

	private void LookAtPlayer()
	{
		Quaternion start = enemy.transform.rotation;
		Quaternion finish = enemy.transform.rotation;
		finish.SetLookRotation (enemy.player.transform.position - enemy.transform.position);

		enemy.transform.rotation = Quaternion.Lerp (start, finish, interp);

		interp += 0.5f * Time.deltaTime;
	}
}
