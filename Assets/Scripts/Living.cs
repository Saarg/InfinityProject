using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapons;

public class Living : MonoBehaviour {

	public float attackRange = 10f;
	public float attackRate = 1f;
	public float attackForce = 10f;

	public Transform sight;
	public float sightRange;
	public Vector3 destination;

  	[SerializeField]
    protected float _life = 100.0F;
	public float life { 
		get { return _life; } 
		set { _life = value > _maxLife ? _maxLife : value ; }
	}

	[SerializeField]
	protected float _maxLife = 100.0F;
	public float maxLife { get { return _maxLife; } }
	[SerializeField] protected float _speed = 6.0F;

	public CharacterController _controller;
	protected Vector3 _moveDirection = Vector3.zero;

	[SerializeField] protected float _gravity = 20.0F;

	[SerializeField] protected Transform _gunAnchor;
	[SerializeField] protected Weapon _gun;
	public Weapon gun { get { return _gun; } }

	protected virtual void Start() {
		_controller = GetComponent<CharacterController>();
	}

	protected virtual void Update() {

	}

	protected virtual void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.transform.tag);
		if (collision.transform.tag == "Projectile") {
			_life -= collision.transform.GetComponent<Bullet> ().GetDamages ();

			if (_life <= 0)
				Destroy (this.gameObject);
		}
        if (collision.transform.tag == "ExplProjectile")
        {
           
        }
    }

	public void PickGun(GameObject g) {
		if (g != null && g.GetComponent<Weapon> () != null) {
			GameObject newGun = Instantiate (g, _gunAnchor.position, _gunAnchor.rotation);
			newGun.transform.SetParent (transform);

			if (_gun != null)
				Destroy (_gun.gameObject);

			_gun = newGun.GetComponent<Weapon> ();
		}
	}

    void ApplyDamage(float damage)
    {
        _life = _life - damage;
        if (_life <= 0)
            Destroy(this.gameObject);
    }
}
