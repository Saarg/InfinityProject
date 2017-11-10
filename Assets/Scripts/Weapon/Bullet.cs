using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	public class Bullet : MonoBehaviour {

		[SerializeField]
		protected BulletSpec _specs;

		protected Rigidbody _rb;

		void Start () {
			_rb = GetComponent<Rigidbody> ();

			if (_rb == null) {
				Debug.LogWarning ("No rigidbody found on bullet, destroying bullet script");
				Destroy (this);
			}
            _rb.velocity = transform.forward * _specs.velocity;
            Destroy(gameObject, _specs.lifespan);
		}



		void OnCollisionEnter(Collision collision) {
            _rb.velocity = new Vector3(0,0,0);

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
			collision.gameObject.SendMessage ("ApplyDamage", GetDamages(), SendMessageOptions.DontRequireReceiver);
		}

		public float GetDamages() {
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
