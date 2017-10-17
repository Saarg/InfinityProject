using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : Living {

	[SerializeField]
	private Transform _target;

	protected override void Start(){
		base.Start ();

		if (_target == null)
			_target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	protected override void Update() 
	{
		base.Update ();

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
