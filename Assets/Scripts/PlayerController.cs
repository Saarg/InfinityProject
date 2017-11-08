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
        Vector3 faceDirection = Vector3.zero;
        
        //Debug.Log("FaceDirection X " + faceDirection.x + " Y " + faceDirection.y +" Z " + faceDirection.z);
        //Debug.Log("_moveDirection X " + _moveDirection.x + " Y " + _moveDirection.y + " Z " + _moveDirection.z);
        if (_moveDirection.z > 0)
        {
            faceDirection.x = 20;
            Debug.Log("Avant");
        }
        else if (_moveDirection.z < 0)
        {
            faceDirection.x = -20;
            Debug.Log("Arriere");
        }
        if (_moveDirection.x > 0)
        {
            faceDirection.z = 20;
            Debug.Log("Droite");
        }
        else if (_moveDirection.x < 0)
        {
            faceDirection.z = -20;
            Debug.Log("Gauche");
        }
        //Debug.Log("_moveDirection X " + _moveDirection.x + " Y " + _moveDirection.y + " Z " + _moveDirection.z);
        
        for (int i = 0; i < 18; i++)
        {
            transform.Rotate(faceDirection);
            //Debug.Log("FaceDirection X " + faceDirection.x + " Y " + faceDirection.y + " Z " + faceDirection.z);
            yield return null;
        }
        
    }
}
