using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {

	protected override void Update() 
	{
		base.Update ();

		Vector3 LookAt = new Vector3(Input.GetAxisRaw("RHorizontal"), 0, -Input.GetAxisRaw("RVertical"));
		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(LookAt);

		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			_moveDirection *= _speed;
		}

		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);

		if (_gun != null && Input.GetButtonDown ("Fire1")) {
			_gun.Fire ();
		}
	}
}
