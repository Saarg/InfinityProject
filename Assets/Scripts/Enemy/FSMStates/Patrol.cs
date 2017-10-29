using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : FSMState {

	public override void Enter(){
		sightColor = Color.green;
	}

	public override void Execute(Enemy enemy){
		Debug.Log ("patrolling");
	}

	public override void Exit(){}

}
