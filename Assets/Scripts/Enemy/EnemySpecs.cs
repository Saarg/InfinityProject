using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemySpecs", order = 1)]
public class EnemySpecs : ScriptableObject {
	public float alertDuration = 3f;

	public float attackRange = 10f;
	public float attackRate = 1f; //cooldown until next attack in seconds
	public float attackForce = 10f;

	public float patrollingSpeed = 1f;
	public float chasingSpeed = 1f;
	public float alertSpeed = 1f;

	public float securityDistance = 2f;

	public float searchTurningSpeed = 120f;

	public float sightRange = 2.5f;
	public float sightAngle = 10f;

	public float wanderingRange = 10f;
}
