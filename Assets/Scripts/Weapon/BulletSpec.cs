using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons{
	[CreateAssetMenu(fileName = "Bullet", menuName = "Weapons/Bullet", order = 1)]
	public class BulletSpec : ScriptableObject {
        public float damage = 1;

		public float velocity;
		public float lifespan;
		public int bounces;
		public float bounceChance;
	}
}