using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapons;

[RequireComponent(typeof(AudioSource))]

/*
 * Main class to all exept boss (enemies ans player)
 */
public class Living : MonoBehaviour {

	[Header("Stats")]
	public float attackRange = 10f;
	public float attackRate = 1f;
	public float attackForce = 10f;

	public Transform sight;
	public float sightRange;
	public Vector3 destination;

	[Header("Resistances")]
	public string weakness;
	public float weaknessFactor;
	public string resistance;
	public float resistanceFactor;

    [SerializeField]
    protected float _life = 100.0F;
	public float life { 
		get { return _life; } 
		set { _life = value > _maxLife ? _maxLife : value ; }
	}
	protected bool _isHit = false;
	public bool IsHit { 
		get { return _isHit; } 
	}

	private Color _color;

	[SerializeField]
	protected float _maxLife = 100.0F;
	public float maxLife { get { return _maxLife; } }
	[SerializeField] protected float _speed = 6.0F;

	[Header("Movement")]
	public CharacterController _controller;
	protected Vector3 _moveDirection = Vector3.zero;

	[SerializeField] protected float _gravity = 20.0F;

	[Header("Gun")]
	[SerializeField] protected Transform _gunAnchor;
	[SerializeField] protected Weapon _gun;
	public Weapon gun { get { return _gun; } }

	[Header("Sounds")]
	public AudioClip stepSound;
	public AudioClip jumpSound;
	public AudioClip hurtSound;
	protected AudioSource _audioSource;

	/*
	 * Init conponents
	 */
	protected virtual void Start() {
		_color = GetComponent<Renderer>().material.color;
        _controller = GetComponent<CharacterController>();
		_audioSource = GetComponent<AudioSource> ();
	}

	/*
	 * Apply damage if under dungeon
	 */
	protected virtual void Update() {
		if (transform.position.y < -10)
			ApplyDamage(-transform.position.y);
	}

	/*
	 * Play sound when hit
	 */
	protected virtual void OnCollisionEnter(Collision collision) {
		_audioSource.enabled = true;
		_audioSource.clip = hurtSound;
		_audioSource.loop = false;
		_audioSource.Play ();
    }

	/*
	 * Equip new gun and destroy old gun
	 */
	public void PickGun(GameObject g) {
		if (g != null && g.GetComponent<Weapon> () != null) {
			GameObject newGun = Instantiate (g, _gunAnchor.position, _gunAnchor.rotation);
			newGun.transform.SetParent (transform);

			if (_gun != null)
				Destroy (_gun.gameObject);

			_gun = newGun.GetComponent<Weapon> ();
		}
	}

	/*
	 * Damage function
	 */
    protected virtual void ApplyDamage(float damage)
    {
        _life = _life - damage;
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
		StartCoroutine(Blink());
    }

	/*
	 * Hit feedback
	 */
    private IEnumerator Blink()
    {
		float time = 0;
		this._isHit = true;

		for (int i = 0; i < 5; i++)
		{
			while (time < 0.1f) {
				time += Time.deltaTime;
				GetComponent<Renderer> ().material.color = Color.Lerp (_color, Color.white, time/0.1f);
				yield return null;
			}
			time = 0;
			while (time < 0.1f) {
				time += Time.deltaTime;
				GetComponent<Renderer> ().material.color = Color.Lerp (Color.white, _color, time/0.1f);
				yield return null;

			}
        }

		GetComponent<Renderer>().material.color = _color;
		this._isHit = false;
    }
}
