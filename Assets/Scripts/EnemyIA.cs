using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : Living {

	[SerializeField]
	private float _gravity = 20.0F;

	private Vector3 _moveDirection = Vector3.zero;
	[SerializeField]
	private CharacterController _controller;
	[SerializeField]
	private Transform _target;

	void Start(){
		_controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		Vector3 LookAt = _target.position - transform.position;

		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(LookAt);
		if (_controller.isGrounded) 
		{
			if (LookAt.magnitude > 3f) {
				LookAt.Normalize ();
				_moveDirection = LookAt;
				_moveDirection *= _speed;
			} else if (LookAt.magnitude < 2.5f) {
				LookAt.Normalize ();
				_moveDirection = -LookAt;
				_moveDirection *= _speed;
			} else {
				_moveDirection = Vector3.zero;
			}
		}

		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);
	}
}
