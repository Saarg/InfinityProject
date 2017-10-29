using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : FSMState {
	
	public override void Enter(){
		sightColor = Color.red;
	}

	public override void Execute(Enemy enemy){
		Vector3 direction = enemy.target.position - enemy.transform.position;

		if(direction != Vector3.zero){
			enemy.transform.rotation = Quaternion.LookRotation(direction);
		}

		if (direction.magnitude > enemy.specs.attackRange) {
			//too far
			enemy.Move (direction);
		} else {
			//close enough to attack
			enemy.Shoot();
		}
	}

	public override void Exit(){}

}
