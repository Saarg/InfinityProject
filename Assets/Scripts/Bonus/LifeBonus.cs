using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBonus : Bonus {

	public float lifeRegen = 20f;

	protected override void Apply(Living entity) {
		entity.life += lifeRegen;

		Destroy (gameObject);
	}
}
