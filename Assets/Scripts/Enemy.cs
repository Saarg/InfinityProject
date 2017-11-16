using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Living {

	public EnemySpecs specs;

	public Transform player;

	public Vector3 lastPlayerKnownLocation;

	private float inverseMoveTime; 	//useful to improve efficiency of calculation
	private float shootingCooldown; //time since the last bullet was fire

	private Animator stateMachine;
	public Color sightColor;


	protected override void Start ()
	{
		lastPlayerKnownLocation = Vector3.zero;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
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
			//Draw sight
			Gizmos.color = sightColor;
			Vector3 centerSight = sight.forward.normalized * specs.sightRange;		
			Quaternion leftRotation	= Quaternion.Euler (0, specs.sightAngle / 2f, 0);
			Vector3 leftSight = leftRotation * centerSight;
			Gizmos.DrawRay(sight.position, leftSight);
			Quaternion rightRotation = Quaternion.Euler (0, specs.sightAngle / -2f, 0);
			Vector3 rightSight = rightRotation * centerSight;
			Gizmos.DrawRay(sight.position, rightSight);

			//Draw hearing area
			Gizmos.DrawWireSphere(transform.position, specs.soundDetectionRange);

			//Draw attack range
			Gizmos.color = Color.magenta;
			Vector3 attackRay = sight.forward.normalized * specs.attackRange;
			Gizmos.DrawRay (sight.position, attackRay);
		}
	}

	/** Move(Vector3 direction) : Try to move to the given direction
	 * return true if the enemy moves
	 * false if the movement didn't occur
	 */
	public bool Move(Vector3 direction)
	{
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
		if (player != null) {
			Vector3 diff = player.position - transform.position;
			float angle = Vector3.Angle (diff, transform.forward);

			if (diff.magnitude < specs.sightRange && angle < (specs.sightAngle / 2f)) 
				return true;
		}
		return false;
	}

	/** PlayerIsHeard() : bool
	 * cast a sphere wrapping the enemy and
	 * return true if the player enters it (even in the back)
	 * return false otherwise
	 */
	public bool PlayerIsHeard()
	{
		if (player != null) {
			Vector3 diff = player.position - transform.position;

			if (diff.magnitude < specs.soundDetectionRange) 
				return true;
		}
		return false;
	}

	/** WallIsSeen() : bool
//	 * cast 1 ray as wall detectors and
//	 * return true if a wall intersect with raycast
//	 * return false otherwise
//	 */
	public bool WallIsSeen()
	{
		Ray ray = new Ray (sight.position, sight.forward.normalized * specs.sightRange);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, specs.sightRange))
		if (hit.collider.CompareTag ("Player"))
			return false;
		else {
//			Vector3 incomingVec = hit.point - transform.position;
//			Vector3 reflect = Vector3.Reflect(incomingVec, hit.normal);
//			Vector3 wallAvoidance = incomingVec + reflect;
//			Debug.DrawRay (transform.position, wallAvoidance, Color.red);
			return true;
		}

		return false;
	}
}
	