using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Living {

	public EnemySpecs specs;
	public Transform target;

	private float inverseMoveTime; //useful to improve efficiency of calculation
	private float shootingCooldown; //time since the last bullet was fire

	private EnemyFSM stateMachine;

	protected void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		shootingCooldown = 0;

		if(stateMachine == null) stateMachine = GetComponent<EnemyFSM> ();

		base.Start ();
	}

	protected void Update()
	{
		base.Update ();

		shootingCooldown += Time.deltaTime;

		if(stateMachine != null)
			stateMachine.Update();
	}

	/** Move(Vector3 direction) : Try to move to the given direction
	 * return true if the enemy moves
	 * false if the movement didn't occur
	 */
	public bool Move(Vector3 direction)
	{
		direction.Normalize ();
		direction *= specs.speed;
		direction.y -= _gravity * Time.deltaTime;
		_controller.Move (direction * Time.deltaTime);
		return true;
	}

	/** Shoot() : Try to use the gun
	 * return true if a shooting accurs
	 * false otherwise
	 */
	public bool Shoot()
	{
		if (shootingCooldown > specs.attackRate && _gun != null) {
			_gun.Fire ();
			shootingCooldown = 0;
			return true;
		} else {
			return false;
		}					
	}
}
	