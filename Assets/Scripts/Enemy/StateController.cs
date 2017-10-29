using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Living
{
	public Transform sight;

	public EnemySpecs enemySpecs;
	public Enemy enemy;
	public Transform target;

	public State currentState;
	public State remainState; //dumb state so that the action "remain in the same Sate" is visible in Unity

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public float stateTimeElapsed;

	void Awake(){}

	void Start(){
//		target = GameObject.FindGameObjectWithTag("Player").transform;
		target = null;
		_moveDirection = Vector3.zero;
	}

	void Update()
	{
		base.Update ();
		currentState.UpdateState(this);

		Vector3 LookAt;

		if (target != null) {
			LookAt = target.position - transform.position;
		} else {
			LookAt = Vector3.zero;
		}

		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation (LookAt);

//		if (_controller.isGrounded) {
//						if (LookAt.magnitude > 3f) {
//							LookAt.Normalize ();
//							_moveDirection = LookAt;
//							_moveDirection *= _speed;
//						} else if (LookAt.magnitude < 2.5f) {
//							LookAt.Normalize ();
//							_moveDirection = -LookAt;
//							_moveDirection *= _speed;
//						} else {
//							_moveDirection = Vector3.zero;
//						}
//		}



		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);

	}

	void OnDrawGuizmos()
	{
		if(currentState != null && sight != null)
		{
			Gizmos.color = currentState.sceneGuizmoColor;
			Gizmos.DrawWireSphere(sight.position, 1);
		}
	}

	public void TransitionToState(State nextState)
	{
		if (nextState != remainState) { //if we really change State only
			currentState = nextState;
			OnExitState ();
		}
	}

	public bool CheckIfCountDownElapsed(float duration)
	{
		stateTimeElapsed += Time.deltaTime;
		return (stateTimeElapsed >= duration);
	}

	private void OnExitState()
	{
		stateTimeElapsed = 0;
	}
}
