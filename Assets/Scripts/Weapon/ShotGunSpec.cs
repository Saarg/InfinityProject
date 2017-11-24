using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
	[CreateAssetMenu(fileName = "ShotGun", menuName = "Weapons/ShotGun", order = 2)]
	public class ShotGunSpec : WeaponSpec {

		public int bulletsPerShot = 3;
		public int spreadAngle = 20;

		public override void Fire(Transform t, Transform owner) {
			Transform canon = t.Find ("Canon");
            Bullet b;

            for (int i = 0; i < bulletsPerShot; i++) {
				GameObject o = Instantiate (ammoPrefab, canon.position, canon.rotation);

				o.transform.Rotate (Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
                b = (Bullet) o.GetComponent(typeof(Bullet));
                b.owner = owner;
            }
		}
	}
}
