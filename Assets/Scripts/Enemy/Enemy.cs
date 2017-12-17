using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Weapons;

/*
 * Enemy class handling AI state machine
 */
public class Enemy : Living {

	public EnemySpecs specs;

	public Transform[] players;
	public Transform target;

	private float inverseMoveTime; 	//useful to improve efficiency of calculation
	private float shootingCooldown; //time since the last bullet was fire

	private Animator stateMachine;
	public Color sightColor;

	public NavMeshAgent nma;

	public RaycastHit wallHitLeft;
	public RaycastHit wallHitRight;
	public bool hitWallLeft;
	public bool hitWallRight;

    [Header("Health Management")]

    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor;
    public Color zeroHealthColor;

    protected override void Start ()
	{
		//init main values
		shootingCooldown = 0;
		sightColor = Color.grey;
		slider.maxValue = maxLife;
		slider.value = maxLife;

		// init all players
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Player");
		players = new Transform[gos.Length];
		for (int i = 0 ; i < gos.Length ; i++)
			players[i] = gos[i].transform;

		//init navmeshagent
		nma = GetComponent<NavMeshAgent> ();
		nma.enabled = true;

		//init state machine
        if (stateMachine == null) stateMachine = GetComponent<Animator> ();

		base.Start ();
	}

	protected override void Update()
	{
		base.Update ();

		//update current health display
        UpdateHealth();

		//update navmeshagent
        if (nma.velocity == Vector3.zero && _audioSource.clip == stepSound)
			_audioSource.Pause ();

		//update useful vars
		shootingCooldown += Time.deltaTime;
    }

	protected override void OnCollisionEnter(Collision collision) {
		base.OnCollisionEnter(collision);

		if (collision.gameObject.GetComponent<Bullet> () != null && collision.gameObject.GetComponent<Bullet> ().owner != null && collision.gameObject.GetComponent<Bullet> ().owner.tag == "Player") {
			target = collision.gameObject.GetComponent<Bullet> ().owner;
		}
	}

	void OnDrawGizmosSelected()
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

			//Draw footsteps detection area
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, specs.soundDetectionRange);

			//Draw shot detection area
			Gizmos.color = Color.gray;
			Gizmos.DrawWireSphere(transform.position, specs.shotDetectionRange);
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
	 * cast a cone as range of sight
	 * return true if the player intersect with raycast
	 * return false otherwise
	 */
	public bool PlayerIsSeen()
	{
		foreach (Transform p in players) {
			if (p != null) {
				Vector3 diff = p.position - transform.position;
				float angle = Vector3.Angle (diff, transform.forward);

				//check if one of the players is in the range of sight of the enemy
				if (diff.magnitude < specs.sightRange && angle < (specs.sightAngle / 2f)) {
					target = p;
					return true;
				}
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
		foreach (Transform p in players) {
			if (p != null) {
				Vector3 diff = p.position - transform.position;

				//if player is close enough, the enemy hear the sounds of its footsteps
				if (diff.magnitude < specs.soundDetectionRange) {
					target = p;
					Debug.Log ("player is heard 1");
					return true;
				}

				//if player fires its weapon close enough of the enemy, the sound is heard
				if (diff.magnitude < specs.shotDetectionRange && p.gameObject.GetComponent<PlayerController> ().HasFired ()) {
					target = p;
					return true;
				}
			}
		}
		return false;
	}


	/** WallIsSeen() : bool
	 * cast 1 ray as wall detectors and
	 * return true if a wall intersect with raycast
	 * return false otherwise
	 */
	public bool WallIsSeen()
	{
		hitWallLeft = false;
		hitWallRight = false;

		Vector3 centerSight = sight.forward.normalized * specs.wallAvoidance;	
		Quaternion leftRotation	= Quaternion.Euler (0, specs.sightAngle / -2f, 0);
		Vector3 leftSight = leftRotation * centerSight;
		Ray left = new Ray (sight.transform.position, leftSight);

		Quaternion rightRotation = Quaternion.Euler (0, specs.sightAngle / 2f, 0);
		Vector3 rightSight = rightRotation * centerSight;
		Ray right = new Ray (sight.transform.position, rightSight);

//		Debug.DrawRay (transform.position, centerSight, Color.magenta);
//		Debug.DrawRay (transform.position, leftSight, Color.magenta);
//		Debug.DrawRay (transform.position, rightSight, Color.magenta);

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

	/** IsUnderAttack() : bool
	 * check if the enemy got hit by the player
	 * return true if so
	 * return false otherwise
 	*/
	public bool IsUnderAttack()
	{
		return (IsHit && target != null);
	}


	/** UpdateHealth() : void
	 * take the current health value and displays it as a circle around the enem
	 */
    public void UpdateHealth()
    {
        slider.value = _life;

        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, _life / maxLife);
    }
}
	