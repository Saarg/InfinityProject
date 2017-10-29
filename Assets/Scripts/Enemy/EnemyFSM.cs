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
		currentState = new Chase ();
	}

	public void Update()
	{
		currentState.Execute (enemy);
	}

	private void ChangeState(FSMState newState)
	{
		currentState = newState;
	}

	//not working alas
	void OnDrawGuizmosSelected()
	{
		Debug.Log ("here");
		if(currentState != null && enemy.sight != null)
		{
			Gizmos.color = currentState.sightColor;
			Gizmos.DrawWireSphere(enemy.transform.position, 10);
		}
	}
}
