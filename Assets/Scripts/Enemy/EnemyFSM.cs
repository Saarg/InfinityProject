using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
	public Enemy enemy;
	public FSMState currentState;

	public void Start()
	{
		enemy = GetComponent<Enemy> ();
		currentState = new Patrol (); //defaultState
		currentState.Enter(enemy);
	}

	public void Update()
	{
		currentState.Execute (enemy);
	}

	public void ChangeState(FSMState newState)
	{
		currentState.Exit (enemy);
		currentState = newState;
		currentState.Enter (enemy);
	}

	void OnDrawGizmos()
	{
		if(currentState != null && enemy.sight != null)
		{
			Gizmos.color = currentState.sightColor;
			Debug.DrawRay (enemy.sight.position, enemy.sight.forward.normalized * enemy.specs.sightRange, currentState.sightColor);
		}
	}
}
