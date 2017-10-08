using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 6.0F;
	[SerializeField]
	private float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	[SerializeField]
	private CharacterController controller;

	void Start(){
		controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		Vector3 LookAt = new Vector3(Input.GetAxisRaw("RHorizontal"), 0, -Input.GetAxisRaw("RVertical"));
		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(LookAt);

		if (controller.isGrounded) 
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
	}
}
