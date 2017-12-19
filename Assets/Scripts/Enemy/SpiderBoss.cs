using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SpiderBoss : MonoBehaviour { 
	[SerializeField]
	private GameObject _target;

	[Header("Arms")]
	[SerializeField]
	private Transform LArmTarget;
	[SerializeField]
	private Transform RArmTarget;
	[SerializeField]
	private AnimationCurve RotateY;
	[SerializeField]
	private AnimationCurve AttackY;
	[SerializeField]
	private AnimationCurve AttackZ;
	private bool LArmBusy;
	private bool RArmBusy;

	[Header("gun arms")]
	[SerializeField]
	private Vector3 gunOffset;
	[SerializeField]
	private Transform LGunArmTarget;
	[SerializeField]
	private Transform RGunArmTarget;

	public LayerMask layerMask;

	[Header("Weapons")]
	[SerializeField]
	private Weapon leftGun;
	[SerializeField]
	private Weapon rightGun;

	// Use this for initialization
	void Start () {
		_target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Look At
		if (Vector3.Angle(transform.forward, _target.transform.position - transform.position) > 10f) {
			Quaternion startRotation = transform.rotation;
			transform.LookAt (_target.transform.position);
			Quaternion targetRotation = transform.rotation;

			transform.rotation = Quaternion.Lerp (startRotation, targetRotation, Time.deltaTime);
		}

		leftGun.transform.LookAt (_target.transform.position);
		leftGun.Fire ();

		rightGun.transform.LookAt (_target.transform.position);
		rightGun.Fire ();

		if ((_target.transform.position - transform.position).magnitude < 7f && !LArmBusy && !RArmBusy) {
			StartCoroutine (LegAttack ());
		} else if (Vector3.Angle(transform.forward, _target.transform.position - transform.position) > 10f && !LArmBusy && !RArmBusy) {
			StartCoroutine (LegWalk ());
		}
	}

	void LateUpdate() {
		Vector3 startRayPos = (transform.position + transform.up + transform.forward);

		// Handle left arms
		RaycastHit hit;
		Vector3 rayDir = LArmTarget.position - startRayPos;
		Ray ray = new Ray (startRayPos, rayDir);
		if (Physics.Raycast (ray, out hit, rayDir.magnitude, layerMask)) {
			LArmTarget.position = hit.point;
		}

		// Handle right arms
		rayDir = RArmTarget.position - (startRayPos);
		ray = new Ray (startRayPos, rayDir);
		if (Physics.Raycast (ray, out hit, rayDir.magnitude, layerMask)) {
			RArmTarget.position = hit.point;
		}

		// Handle left gun arms
		Vector3 offset = gunOffset;
		LGunArmTarget.position = startRayPos + (_target.transform.position + offset - startRayPos)/2;

		// Handle right gun arms
		offset = Vector3.Scale(offset, new Vector3(-1, 1, 1));
		RGunArmTarget.position = startRayPos + (_target.transform.position + offset - startRayPos)/2;
	}

	IEnumerator LegWalk() {
		if (!LArmBusy && !RArmBusy) {
			LArmBusy = true;
			RArmBusy = true;

			float startTime = Time.realtimeSinceStartup;
			Vector3 LstartPos = LArmTarget.localPosition;
			Vector3 RstartPos = RArmTarget.localPosition;

			float time = Time.realtimeSinceStartup - startTime;
			while (time < 1f) {
				LArmTarget.localPosition = LstartPos + Vector3.up * RotateY.Evaluate (Time.realtimeSinceStartup % 1f);
				RArmTarget.localPosition = RstartPos + Vector3.up * RotateY.Evaluate ((Time.realtimeSinceStartup + 0.5f) % 1f);

				yield return new WaitForSeconds (0.01f);
				time = Time.realtimeSinceStartup - startTime;
			}

			LArmTarget.localPosition = LstartPos;
			RArmTarget.localPosition = RstartPos;

			LArmBusy = false;
			RArmBusy = false;
		}
	}

	IEnumerator LegAttack() {
		Transform target = null;

		// Choose wich leg should attack
		float angle = Vector3.SignedAngle(transform.forward, _target.transform.position - transform.position, Vector3.up);
		if (!LArmBusy && angle < 0) {
			LArmBusy = true;

			target = LArmTarget;
		} else if (!RArmBusy && angle > 0) {
			RArmBusy = true;

			target = RArmTarget;
		} 

		if (target != null) {
			Debug.Log ("Attacking!");

			float startTime = Time.realtimeSinceStartup;
			Vector3 startWorlPos = target.position;
			Vector3 startLocaPos = target.localPosition;

			float time = Time.realtimeSinceStartup - startTime;
			while (time < 1f) {
				Debug.DrawLine (startWorlPos, 
					startWorlPos + (_target.transform.position - startWorlPos) * AttackZ.Evaluate (time / 1f), 
					Color.red, 0.05f);

				target.position = startWorlPos;
				target.position += (_target.transform.position - startWorlPos) * AttackZ.Evaluate (time / 1f);
				target.position += Vector3.up * AttackY.Evaluate (time / 1f);

				yield return new WaitForSeconds (0.05f);
				time = Time.realtimeSinceStartup - startTime;
			}

			startTime = Time.realtimeSinceStartup;
			time = Time.realtimeSinceStartup - startTime;

			while (time < 1f) {
				target.localPosition = Vector3.Lerp (target.localPosition, startLocaPos, time / 1f);

				yield return new WaitForSeconds (0.05f);
				time = Time.realtimeSinceStartup - startTime;
			}

			target.localPosition = startLocaPos;

			LArmBusy = target == LArmTarget ? false : LArmBusy;
			RArmBusy = target == RArmTarget ? false : RArmBusy;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere (transform.position, 7f);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (LArmTarget.transform.position, 0.1f);
		Gizmos.DrawWireSphere (RArmTarget.transform.position, 0.1f);
		Gizmos.DrawWireSphere (LGunArmTarget.transform.position, 0.1f);
		Gizmos.DrawWireSphere (RGunArmTarget.transform.position, 0.1f);
	}
}
