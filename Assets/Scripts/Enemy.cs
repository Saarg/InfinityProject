using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Living {

	public EnemySpecs specs;

	public Transform player;

	public Vector3 lastPlayerKnownLocation;

	private float inverseMoveTime; 	//useful to improve efficiency of calculation
	private float shootingCooldown; //time since the last bullet was fire

	private Animator stateMachine;
	public Color sightColor;

	public NavMeshAgent nma;

	public RaycastHit wallHitLeft;
	public RaycastHit wallHitRight;
	public bool hitWallLeft;
	public bool hitWallRight;

	protected override void Start ()
	{
		lastPlayerKnownLocation = Vector3.zero;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nma = GetComponent<NavMeshAgent> ();
		shootingCooldown = 0;
		sightColor = Color.grey;

		if (stateMachine == null) stateMachine = GetComponent<Animator> ();

		base.Start ();
	}

	protected override void Update()
	{
		base.Update ();

		if (nma.velocity == Vector3.zero && _audioSource.clip == stepSound)
			_audioSource.Pause ();

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
//			Gizmos.color = Color.magenta;
//			Vector3 attackRay = sight.forward.normalized * specs.attackRange;
//			Gizmos.DrawRay (sight.position, attackRay);
		}
	}

	/** Move(Vector3 direction) : Try to move to the given direction
	 * return true if the enemy moves
	 * false if the movement didn't occur
	 */
	public bool Move(Vector3 direction)
	{
		direction.y -= _gravity * Time.deltaTime;
		nma.Move(direction * Time.deltaTime);
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
			{
				Vector3 enemyToPlayer = player.transform.position - transform.position;
				Ray vision = new Ray (transform.position, enemyToPlayer.normalized);
				RaycastHit hit;

				if(Physics.Raycast(vision, out hit, specs.sightRange))
				if(hit.collider.CompareTag("Player"))
					return true;

				return false;
			}
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
		hitWallLeft = false;
		hitWallRight = false;

		Vector3 centerSight = sight.forward.normalized * specs.wallAvoidance;	
		Quaternion leftRotation	= Quaternion.Euler (0, specs.sightAngle / -2f, 0);
		Vector3 leftSight = leftRotation * centerSight;
		Ray left = new Ray (transform.position, leftSight);

		Quaternion rightRotation = Quaternion.Euler (0, specs.sightAngle / 2f, 0);
		Vector3 rightSight = rightRotation * centerSight;
		Ray right = new Ray (transform.position, rightSight);

		Debug.DrawRay (transform.position, centerSight, Color.magenta);
		Debug.DrawRay (transform.position, leftSight, Color.magenta);
		Debug.DrawRay (transform.position, rightSight, Color.magenta);

//		if (Physics.Raycast (left, out wallHitLeft, specs.sightRange) || Physics.Raycast(right, out wallHitRight, specs.sightRange))
//		if (wallHit.collider.CompareTag ("Player"))
//			return false;
//		else {
//			return true;
//		}

		if (Physics.Raycast (left, out wallHitLeft, specs.wallAvoidance))
		if (wallHitLeft.collider.CompareTag ("Player")) {
			hitWallLeft = false;
		} else {
			hitWallLeft = true;
			return true;
		}

		if (Physics.Raycast (right, out wallHitRight, specs.wallAvoidance))
		if (wallHitRight.collider.CompareTag ("Player")) {
			hitWallRight = false;
		} else {
			hitWallRight = true;
			return true;
		}

		return false;
	}
}
	