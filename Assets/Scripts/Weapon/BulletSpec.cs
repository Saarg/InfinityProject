using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons{
	[CreateAssetMenu(fileName = "Bullet", menuName = "Weapons/Bullet", order = 1)]
	public class BulletSpec : ScriptableObject {
        public float damage = 15;

		public float velocity = 6;
		public float lifespan = 10;

		public GameObject onHitParticles;

		public bool isExplosive = false;
		public float areaofeffect = 1;
		public float explosiveDamage = 8;
	}
}