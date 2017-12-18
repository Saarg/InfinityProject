using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {

	/*
	 * Weapon projectiles script
	 */
	public class Bullet : MonoBehaviour {

		[SerializeField]
		protected BulletSpec _specs;

		protected Rigidbody _rb;

        protected Vector3 start;
        protected Vector3 End;

        protected float distance;

		public Transform owner;

        public string Type;

		/*
		 * Get components + init vars
		 */
        void Start () {
			_rb = GetComponent<Rigidbody> ();
            start = this.transform.position;

			if (_rb == null) {
				Debug.LogError ("No rigidbody found on bullet, destroying bullet");
				Destroy (gameObject);
			}

			if (owner == null) {
				Debug.LogError ("No owner found on bullet, destroying bullet");
				Destroy (gameObject);
			}

            _rb.velocity = transform.forward * _specs.velocity;
            Destroy(gameObject, _specs.lifespan);
		}

		/*
		 * Destroy if too slow (like when the boss meteor stopped it)
		 */
		void Update() {
			if (_rb.velocity.magnitude < 1) {
				Destroy (gameObject);
			}
		}

		/*
		 * Apply particles and damages if needed
		 */
		void OnCollisionEnter(Collision collision) {
            _rb.velocity = new Vector3(0,0,0);

			if (_specs.onHitParticles != null)
				Destroy (Instantiate (_specs.onHitParticles, transform.position, Quaternion.identity), 1);

			ImpactDamage (collision);

			if (_specs.isExplosive) {
				AreaDamage();
			}

            Destroy (gameObject);
		}

		/*
		 * Damages 
		 */
		public void ImpactDamage(Collision collision) {
            float damages = GetDamages ();
			Living living = collision.gameObject.GetComponent<Living> ();

			if (living != null) {
				if (String.Compare (living.weakness, this.Type) == 0) {
					damages *= living.weaknessFactor;
				}
				if (String.Compare (collision.gameObject.GetComponent<Living> ().resistance, this.Type) == 0) {
					damages *= living.resistanceFactor;
				}
			}

			collision.gameObject.SendMessage ("ApplyDamage", damages, SendMessageOptions.DontRequireReceiver);

			if (collision.gameObject.tag == "Enemy" && owner != null && owner.tag == "Player")
            {
                End = this.transform.position;
                distance = Vector3.Distance(start, End);
                StatManager.Instance.Ran.Count += 1 + (int)distance;
            }
			else if (collision.gameObject.tag == "Player") {
				StatManager.Instance.Hp.Count++;
			}
		}

		public float GetDamages() {
			// If owner is player take stats into account
			if (owner != null && owner.tag == "Player") {
				return _specs.damage + StatManager.Instance.Atk.Level;
			} else if (owner != null && owner.tag != "Player") {
				StatManager.Instance.Atk.Count++;
			}
			return _specs.damage;
		}

		/*
		 * Explosive Damages 
		 */
		public float GetArea()
		{
			return _specs.areaofeffect;
		}

		public float GetExplosiveDamages() {
			return _specs.explosiveDamage;
		}

		public void AreaDamage()
		{
			Collider[] colls = Physics.OverlapSphere(transform.position, GetArea());

			foreach (Collider col in colls)
			{
				if (col.CompareTag("Enemy"))
				{
					float distance = Vector3.Distance(col.transform.position, transform.position);
					if (distance <=  GetArea() && distance > GetArea()*0.6)
					{
						//Deal 50% dmg to the enemy
						col.SendMessage("ApplyDamage", GetExplosiveDamages()*0.5, SendMessageOptions.DontRequireReceiver);

					}
					else if(distance <= GetArea()*0.6 && distance > GetArea() * 0.3) {
						//Deal 80% dmg to the enemy
						col.SendMessage("ApplyDamage", GetExplosiveDamages()*0.8, SendMessageOptions.DontRequireReceiver);

					}
					else if(distance <= GetArea()*0.3 && distance > 0) {
						//Deal 100% dmg to the enemy
						col.SendMessage("ApplyDamage", GetExplosiveDamages(), SendMessageOptions.DontRequireReceiver);

					}
				}
			}
		}
	}
}
