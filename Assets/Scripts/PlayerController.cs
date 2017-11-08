﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {

    [SerializeField] protected float _jumpSpeed = 6;
    [SerializeField] protected float _jumpHeight = 4;

    protected override void Update() 
	{
		base.Update ();

		/*
		 * Look at
		 */
		if (_controller.isGrounded) {
			Vector3 LookAt = new Vector3 (Input.GetAxisRaw ("RHorizontal"), 0, -Input.GetAxisRaw ("RVertical"));
			if (LookAt != Vector3.zero)
				transform.rotation = Quaternion.LookRotation (LookAt);
			else { // If no controler use mouse
				Vector3 cursorPos = Input.mousePosition;

				Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (transform.position);

				Vector3 PtoC = cursorPos - playerScreenPos;
				PtoC.Normalize ();

				PtoC.z = PtoC.y;
				PtoC.y = 0;

				transform.rotation = Quaternion.LookRotation (PtoC);

				Debug.DrawLine (transform.position, transform.position + PtoC, Color.grey);
			}
		}

		/*
		 * Movement
		 */
		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            

            if (Input.GetKeyDown("space"))
            {
                _moveDirection *= _jumpSpeed;
                _moveDirection.y = _jumpHeight;
                StartCoroutine("Jump");
            }
            else
            {
                _moveDirection *= _speed;
            }
		}
        else
        {
            
        }

		/*
		 * Apply Movement
		 */
		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);

		/*
		 * Gun managment
		 */
		if (_gun != null && Input.GetButtonDown ("Fire1")) {
			_gun.Fire ();
		}
		
		if (_gun != null && Input.GetButtonDown ("Reload")) {
			_gun.StartReload ();
		}
	}

    IEnumerator Jump()
    {
        Debug.Log("Coroutine de saut");

		Vector3 planeModeDir = _moveDirection;
		planeModeDir.y = 0;

		Vector3 rotationAxis = Quaternion.AngleAxis (90, Vector3.up) * planeModeDir;
		rotationAxis.Normalize ();
        
        for (int i = 0; i < 18; i++)
        {
			transform.RotateAround (transform.position, rotationAxis, 20);
            //Debug.Log("FaceDirection X " + faceDirection.x + " Y " + faceDirection.y + " Z " + faceDirection.z);
			yield return new WaitForSeconds(0.01f);
        }
        
    }
}
