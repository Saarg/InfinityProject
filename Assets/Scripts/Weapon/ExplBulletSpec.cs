using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons{
    [CreateAssetMenu(fileName = "ExplBullet", menuName = "Weapons/ExplBullet", order = 1)]
    public class ExplBulletSpec : ScriptableObject{
        public float damage = 1;

        public float velocity;
        public float lifespan;
        public int bounces;
        public float bounceChance;
        public float Areaofeffect;
    }
}