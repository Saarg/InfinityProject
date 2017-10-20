using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : Living {

	private static List<EnemyIA> _enemyList = new List<EnemyIA>();

	[SerializeField]
	private Transform _target;

	protected override void Start(){
		base.Start ();

		_enemyList.Add (this);

		if (_target == null)
			_target = GameObject.FindGameObjectWithTag ("Player").transform;

		if (_gun != null)
			StartCoroutine (GunUpdate());
	}

	protected void OnDestroy() {
		_enemyList.Remove (this);
	}

	protected override void Update() 
	{
		base.Update ();

		Vector3 LookAt = _target.position - transform.position;

		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(LookAt);
		if (_controller.isGrounded) 
		{
			if (LookAt.magnitude > 3f) {
				LookAt.Normalize ();
				_moveDirection = LookAt;
				_moveDirection *= _speed;
			} else if (LookAt.magnitude < 2.5f) {
				LookAt.Normalize ();
				_moveDirection = -LookAt;
				_moveDirection *= _speed;
			} else {
				_moveDirection = Vector3.zero;
			}
		}

		_moveDirection.y -= _gravity * Time.deltaTime;

		Vector3 EvadeDir = Vector3.zero;
		foreach (EnemyIA e in _enemyList) {
			EvadeDir += transform.position - e.transform.position;
		}
		EvadeDir /= _enemyList.Count;
		EvadeDir.Normalize ();

		_controller.Move((0.4f*EvadeDir.normalized + _moveDirection) * Time.deltaTime);
	}

	protected IEnumerator GunUpdate()
	{
		while (_gun != null) {
			_gun.Fire ();

			yield return new WaitForSeconds (_gun.GetFireRate());
		}
	}
}
