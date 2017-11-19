using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEngine.AI;

public class Boss : MonoBehaviour {

	[SerializeField] private Transform player;
	private NavMeshAgent nma;

	[Header("Weapons")]
	[SerializeField] protected float gravity = 20.0F;
	[SerializeField] protected float fireballStrength = 20.0F;
	public float getFireballStrength(){
		return fireballStrength;
	}

	[Header("Weapons")]
	[SerializeField] private Weapon weapon1;
	[SerializeField] private GameObject weapon1Ammo;
	[SerializeField] private Weapon weapon2;
	[SerializeField] private GameObject weapon2Ammo;
	[SerializeField] private GameObject fireball;

	[Header("Stats")]
	[SerializeField] protected float attackRange;
	[SerializeField] protected float fireballCooldown;
	[SerializeField] protected float fireballTime;
	[SerializeField] protected float barrageCooldown;
	[SerializeField] protected float barrageTime;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		weapon1Ammo = weapon1.getSpecs ().ammoPrefab;
		weapon2Ammo = weapon2.getSpecs ().ammoPrefab;

		fireballCooldown = 5f;

		barrageCooldown = 3f;

		fireballTime = 0;
		barrageTime = 0;
	}
	
	void Update () {
		Vector3 direction = player.position - transform.position;
		//always look at the player
		if(direction != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(direction);

		//update special attacks times
		fireballTime += Time.deltaTime;
		barrageTime += Time.deltaTime;

		if (fireballTime > fireballCooldown)
		{
			Fireball ();

			fireballTime = 0;
			return;
		}

		if(barrageTime > barrageCooldown)
		{
			if(weapon1) Barrage (weapon1);
			if(weapon2) Barrage (weapon2);

			barrageTime = 0;
			return;
		}

//		Shoot ();
		
	}

	/** Move(Vector3 direction) : Try to move to the given direction
	 * return true if the enemy moves
	 * false if the movement didn't occur
	 */
	public bool Move(Vector3 direction)
	{
		direction.y -=  gravity * Time.deltaTime;
		nma.Move(direction * Time.deltaTime);
		return true;
	}

	private void Shoot()
	{
		if (weapon1 != null)
			weapon1.Fire ();

		if (weapon2 != null)
			weapon2.Fire ();	
	}

	private void Fireball()
	{
		Vector3 pos = player.position;
		pos.y += 20;
		Quaternion rot = Quaternion.Euler (new Vector3 (90, 0, 0));
		Instantiate(fireball, pos, rot);
	}

	private void Barrage(Weapon w)
	{
		Transform canon = w.transform.Find ("Canon");
		int i;
		for (i = -20; i < 21; i += 5) {
			Vector3 rot = canon.rotation.eulerAngles;
			rot.y += i;
			Instantiate(w.getSpecs().ammoPrefab, canon.position, Quaternion.Euler(rot));
		}
	}
}
