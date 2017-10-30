using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : FSMState {

	[SerializeField] private float jitter = 1f;
	[SerializeField] private float maxLengthJitter = 2f;

	private Vector3 patrolDestination;
	private float elapsedTime;

	public override void Enter(Enemy enemy){
		sightColor = Color.green;
		patrolDestination = ChooseNewDestination (enemy);
		elapsedTime = 0;
	}

	public override void Execute(Enemy enemy){
		elapsedTime += Time.deltaTime;

		if (PlayerIsSeen(enemy))
			enemy.StateMachine ().ChangeState (new Chase ());
		else {
			Vector3 diff = patrolDestination - enemy.transform.position;

			if (diff.magnitude < 1f) {
				ChooseNewDestination (enemy);
			}

			//		enemy.Move (patrolDestination);
		}
	}

	public override void Exit(Enemy enemy){}

	private Vector3 ChooseNewDestination(Enemy enemy)
	{
		float x = Random.Range (jitter*-1, jitter) * Random.Range(0,maxLengthJitter);
		float y = Random.Range (jitter*-1, jitter) * Random.Range(0,maxLengthJitter);
		float z = Random.Range (jitter*-1, jitter) * Random.Range(0,maxLengthJitter);
		patrolDestination = new Vector3 (x, y, z);

		return patrolDestination;
	}

	private bool PlayerIsSeen(Enemy enemy)
	{
		Ray ray = new Ray (enemy.sight.position, enemy.sight.forward.normalized * enemy.specs.sightRange);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, enemy.specs.sightRange))
			if(hit.collider.CompareTag ("Player"))
				return true;

		return false;
	}
}
