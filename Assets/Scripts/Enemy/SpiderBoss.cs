using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class SpiderBoss : MonoBehaviour { 
	private GameObject[] _players;
	private GameObject _target1;
	private GameObject _target;

	[Header("Health Management")]
	public float maxLife = 800f;
	public float life = 800f;
	public Slider slider;
	public Image fillImage;
	public Color fullHealthColor;
	public Color zeroHealthColor;

	[Header("Spawn animation")]
	[SerializeField]
	private float spawnLength = 1f;
	[SerializeField]
	private AnimationCurve SpawnY;
	[SerializeField]
	private AnimationCurve SpawnZ;

	[Header("Arms")]
	[SerializeField]
	private float armAnimationLength = 1f;
	[SerializeField]
	private float walkAnimationLength = 1f;
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

	private bool ready = false;

	// Use this for initialization
	void Start () {
		_players = GameObject.FindGameObjectsWithTag ("Player");
		_target = null;

		maxLife += (int)GameMode.difficulty * 100;
		life = maxLife;
		slider.maxValue = maxLife;
		slider.value = maxLife;

		StartCoroutine (Spawn());
	}

	void Update() {
		if (_target == null) {
			foreach (GameObject go in _players) {
				if (go != null && (_target == null || _target.GetComponent<Living> ().life > go.GetComponent<Living> ().life)) {
					_target = go;
				}
			}
		}

		if (ready && _target != null) {
			// Look At
			float angle = Vector3.Angle (transform.forward, _target.transform.position - transform.position);
			if (angle > 10f) {
				Vector3 lookAt = _target.transform.position;
				lookAt.y = lookAt.y < 0 ? 0 : lookAt.y;

				Quaternion startRotation = transform.rotation;
				transform.LookAt (lookAt);
				Quaternion targetRotation = transform.rotation;

				transform.rotation = Quaternion.Lerp (startRotation, targetRotation, Time.deltaTime);
			}

			leftGun.transform.LookAt (_target.transform.position);
			leftGun.Fire ();

			rightGun.transform.LookAt (_target.transform.position);
			rightGun.Fire ();

			if (!LArmBusy && !RArmBusy) {
				foreach (GameObject go in _players) {
					if (go != null && (go.transform.position - transform.position).magnitude < 7f) {
						_target = go;
						StartCoroutine (LegAttack ());
						break;
					}
				}
			} 
		} else if (_target != null) {
			leftGun.transform.LookAt (_target.transform.position);
			rightGun.transform.LookAt (_target.transform.position);
		}

		if (_target != null && Vector3.Angle (transform.forward, _target.transform.position - transform.position) > 10f && !LArmBusy && !RArmBusy) {
			StartCoroutine (LegWalk ());
		}

		UpdateHealth ();
	}

	void LateUpdate() {
		Vector3 startRayPos = (transform.position + transform.up + transform.forward);

		if (ready) {
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
		}

		if (_target != null) {
			// Handle left gun arms
			Vector3 offset = gunOffset;
			LGunArmTarget.position = startRayPos + (_target.transform.position + offset - startRayPos) / 2;

			// Handle right gun arms
			offset = Vector3.Scale (offset, new Vector3 (-1, 1, 1));
			RGunArmTarget.position = startRayPos + (_target.transform.position + offset - startRayPos) / 2;
		} else {
			LGunArmTarget.transform.position += Vector3.up * Time.deltaTime;
			RGunArmTarget.transform.position += Vector3.up * Time.deltaTime;
		}
	}

	IEnumerator Spawn() {
		float startTime = Time.realtimeSinceStartup;
		Vector3 startPos = transform.position;

		while(Time.realtimeSinceStartup - startTime < spawnLength) {
			transform.position = startPos;
			transform.position += Vector3.up * SpawnY.Evaluate((Time.realtimeSinceStartup - startTime) / spawnLength);
			transform.position += Vector3.forward * SpawnZ.Evaluate((Time.realtimeSinceStartup - startTime) / spawnLength);

			yield return new WaitForEndOfFrame ();

			Vector3 startRayPos = (transform.position + transform.up + transform.forward);

			yield return null;
		}
		ready = true;
	}

	IEnumerator LegWalk() {
		if (!LArmBusy && !RArmBusy) {
			LArmBusy = true;
			RArmBusy = true;

			float startTime = Time.realtimeSinceStartup;
			Vector3 LstartPos = LArmTarget.localPosition;
			Vector3 RstartPos = RArmTarget.localPosition;

			float time = Time.realtimeSinceStartup - startTime;
			while (time < walkAnimationLength) {
				LArmTarget.localPosition = LstartPos + Vector3.up * RotateY.Evaluate (Time.realtimeSinceStartup % walkAnimationLength);
				RArmTarget.localPosition = RstartPos + Vector3.up * RotateY.Evaluate ((Time.realtimeSinceStartup + walkAnimationLength/2f) % walkAnimationLength);

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
			while (_target != null && time < armAnimationLength && (_target.transform.position - transform.position).magnitude < 8f) {
				Debug.DrawLine (startWorlPos, 
					startWorlPos + (_target.transform.position - startWorlPos) * AttackZ.Evaluate (time / armAnimationLength), 
					Color.red, 0.05f);

				target.position = startWorlPos;
				target.position += (_target.transform.position - startWorlPos) * AttackZ.Evaluate (time / armAnimationLength);
				target.position += Vector3.up * AttackY.Evaluate (time / 1f);

				yield return new WaitForSeconds (0.05f);
				time = Time.realtimeSinceStartup - startTime;
			}

			startTime = Time.realtimeSinceStartup;
			time = Time.realtimeSinceStartup - startTime;

			while (target != null && time < 1f) {
				target.localPosition = Vector3.Lerp (target.localPosition, startLocaPos, time / 1f);

				yield return new WaitForSeconds (0.05f);
				time = Time.realtimeSinceStartup - startTime;
			}

			target.localPosition = startLocaPos;

			LArmBusy = target == LArmTarget ? false : LArmBusy;
			RArmBusy = target == RArmTarget ? false : RArmBusy;
		}
	}

	void ApplyDamage(float damage)
	{
		life -= damage;
		if (life <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	public void UpdateHealth(){
		slider.value = life;

		fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, life / maxLife);
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
