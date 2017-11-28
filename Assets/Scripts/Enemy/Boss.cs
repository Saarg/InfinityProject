using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour { 

	[SerializeField] private Transform player;
	private NavMeshAgent nma;

	[Header("Useful")]
	[SerializeField] protected float gravity = 20.0F;
	[SerializeField] protected float fireballStrength = 20.0F;
	public float getFireballStrength(){
		return fireballStrength;
	}

	[Header("Weapons")]
	[SerializeField] private Weapon weapon1;
	[SerializeField] private ParticleSystem preloadWeapon1;

	[SerializeField] private Weapon weapon2;
	[SerializeField] private ParticleSystem preloadWeapon2;

	[SerializeField] private GameObject fireball;

	[Header("Stats")]
	protected float attackRange;
	[SerializeField] protected float fireballCooldown;
	protected float fireballTime;
	[SerializeField] protected float barrageCooldown;
	protected float barrageTime;
	[SerializeField] protected float preloadCooldown;
	protected float preloadTime;

	[Header("Health Management")]
	public float maxLife = 100f;
	[SerializeField] protected float _life;
	public Slider slider;
	public Image fillImage;
	public Color fullHealthColor;
	public Color zeroHealthColor;

	private IEnumerator coroutine;
	private Color col;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		GetComponent<NavMeshAgent> ().enabled = true;

		_life = maxLife;
		slider.maxValue = maxLife;
		slider.value = maxLife;

		fireballCooldown = 5f;
		fireballTime = 0;

		barrageCooldown = 3f;
		barrageTime = 0;

		preloadCooldown = 1.5f;
		preloadTime = 0;

		col = this.GetComponent<Renderer>().material.color;
	}
	
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		Vector3 direction = player.position - transform.position;
		//always look at the player
		if(direction != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(direction);

		//update special attacks times
		fireballTime += Time.deltaTime;
		barrageTime += Time.deltaTime;

		if (fireballTime > fireballCooldown) //fireball is ready
		{
			Fireball ();

			fireballTime = 0;
			return;
		}

		if (barrageTime > barrageCooldown) { //barrage is ready
			if (preloadTime < preloadCooldown) {
				//preload
				preloadWeapon1.Play();
				preloadWeapon2.Play();
				
				preloadTime += Time.deltaTime;
			} else {
				//stop preload
				preloadWeapon1.Stop ();
				preloadWeapon2.Stop ();

				//barrage
				if (weapon1) Barrage (weapon1);
				if (weapon2) Barrage (weapon2);

				barrageTime = 0;
				preloadTime = 0;
			}
			return;
		}
	
		Shoot ();

		UpdateHealth ();
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
			Instantiate(w.getSpecs().ammoPrefab, canon.position, Quaternion.Euler(rot)).GetComponent<Bullet>().owner = transform;
		}
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		//TODO add sound to boss !
//		_audioSource.clip = hurtSound;
//		_audioSource.loop = false;
//		_audioSource.Play ();
	}
	 
	void ApplyDamage(float damage)
	{
		coroutine = Blink();
		StartCoroutine(coroutine);
		_life = _life - damage;
		if (_life <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	private IEnumerator Blink()
	{
		for (int i = 0; i < 5; i++)
		{
			this.GetComponent<Renderer>().material.color = Color.white;
			yield return new WaitForSeconds(.1f);
			this.GetComponent<Renderer>().material.color = col;
			yield return new WaitForSeconds(.1f);
		}
		this.GetComponent<Renderer>().material.color = col;
	}

	public void UpdateHealth(){
		slider.value = _life;

		fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, _life / maxLife);
	}
}
