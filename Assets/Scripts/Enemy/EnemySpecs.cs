using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemySpecs", order = 1)]
public class EnemySpecs : ScriptableObject {
	public float speed = 1f;
	public float turnSpeed = 180f;

	public float alertDuration = 3f;

	public float attackRange = 10f;
	public float attackRate = 1f; //cooldown until next attack in seconds
	public float attackForce = 10f;

	public float moveTime = 0.1f; //time for the object to move in sec

	public float securityDistance = 2f;

	public float searchTurningSpeed = 120f;
	public float searchDuration = 3f; //time before returning to patrol after engaging player

	public float sightRange = 2.5f;
	public float sightSphereCastRadius = 1f;
}
