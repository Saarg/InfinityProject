using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Living {

	public EnemySpecs specs;
	public Transform target;

	public Vector3 lastPlayerKnownLocation;

	private float inverseMoveTime; 	//useful to improve efficiency of calculation
	private float shootingCooldown; //time since the last bullet was fire

	private Animator stateMachine;
	public Color sightColor;


	protected override void Start ()
	{
		lastPlayerKnownLocation = Vector3.zero;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		shootingCooldown = 0;
		sightColor = Color.grey;

		if (stateMachine == null) stateMachine = GetComponent<Animator> ();

		base.Start ();
	}

	protected override void Update()
	{
		base.Update ();

		shootingCooldown += Time.deltaTime;
	}

	void OnDrawGizmos()
	{
		if(sight != null && stateMachine != null)
		{
			Gizmos.color = sightColor;
			Gizmos.DrawRay(sight.position, sight.forward.normalized * specs.sightRange);
		}
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

	/** PlayerIsSeen() : bool
	 * cast a ray as line of sight and
	 * return true if the player intersect with raycast
	 * return false otherwise
	 */
	public bool PlayerIsSeen()
	{
		Ray ray = new Ray (sight.position, sight.forward.normalized * specs.sightRange);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, specs.sightRange))
		if (hit.collider.CompareTag ("Player")) {
			lastPlayerKnownLocation = hit.collider.transform.position;
			return true;
		}

		return false;
	}
}
	