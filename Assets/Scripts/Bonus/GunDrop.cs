using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : Bonus {

	public GameObject gun;

	protected override void Apply(Living entity) {
		entity.PickGun (gun);

		Destroy (gameObject);
	}
}
