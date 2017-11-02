using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class ExplBullet : MonoBehaviour
    {

        [SerializeField]
        private ExplBulletSpec _specs;
        private Transform explosive;
        private Rigidbody _rb;

        void Awake()
        {
            explosive = transform;
        }

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                Debug.LogWarning("No rigidbody found on bullet, destroying bullet script");
                Destroy(this);
            }
            _rb.velocity = transform.forward * _specs.velocity;
            Destroy(gameObject, _specs.lifespan);
        }



        void OnCollisionEnter(Collision collision)
        {
            AreaDamage();
            Destroy(gameObject);
        }

        public float GetDamages()
        {
            return _specs.damage;
        }

        public float GetArea()
        {
            return _specs.Areaofeffect;
        }

        public void AreaDamage()
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, this.GetArea());
            foreach (Collider col in colls)
            {
                if (col.CompareTag("Enemy"))
                {
                   float distance = Vector3.Distance(col.transform.position, explosive.position);
                    if (distance <=  GetArea() && distance > GetArea()*0.6)
                    {
                        //Deal 50% dmg to the enemy
                        col.SendMessage("ApplyDamage", GetDamages()*0.5, SendMessageOptions.DontRequireReceiver);

                    }
                    else if(distance <= GetArea()*0.6 && distance > GetArea() * 0.3) {
                        //Deal 80% dmg to the enemy
                        col.SendMessage("ApplyDamage", GetDamages()*0.8, SendMessageOptions.DontRequireReceiver);
                    }
                    else if(distance <= GetArea()*0.3 && distance > 0) {
                        //Deal 100% dmg to the enemy
                       col.SendMessage("ApplyDamage",GetDamages(), SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }
}
