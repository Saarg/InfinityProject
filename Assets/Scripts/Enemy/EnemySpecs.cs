using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemySpecs", order = 1)]
public class EnemySpecs : ScriptableObject {

//	[Header("Sensors")]
	public float sightRange = 2.5f;
	public float sightAngle = 10f;
	public float soundDetectionRange = 1.5f;
	public float securityDistance = 2f;
	public float wallAvoidance = 1.5f;
	public float shotDetectionRange = 2.5f;


//	[Header("Patrol")]
	public float patrollingSpeed = 1f;
	public float patrollingTurningSpeed = 0.5f;
	public float wanderingRange = 10f;


//	[Header("Chase")]
	public float attackRange = 10f;
	public float attackRate = 1f; //cooldown until next attack in seconds
	public float attackForce = 10f;
	public float chasingSpeed = 1f;
	public float chasingTurningSpeed = 0.5f;


//	[Header("Alert")]
	public float alertDuration = 3f;
	public float alertSpeed = 1f;
	public float alertTurningSpeed = 120f;
}
