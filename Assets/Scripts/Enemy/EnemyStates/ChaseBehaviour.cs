using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : EnemyState {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.red;
		animator.SetBool ("PlayerIsSeen", true);
		animator.SetBool ("PlayerIsDead", false);
		animator.SetBool ("PlayerIsLost", false);

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen ())
			Chase ();
		else
			animator.SetBool ("PlayerIsSeen", false);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

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
}
