using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player controller inherits Living and handles all player movement and stats
 */
public class PlayerController : Living {    
    
    [Header("Player Stats")]
    [SerializeField] protected float _jumpSpeed = 6;
	[SerializeField] protected float _jumptime = 1f;
	[SerializeField] protected AnimationCurve _heightCurve;
	[SerializeField] protected AnimationCurve _rollCurve;

	protected float _stamina = 10;
	public float stamina { get {return _stamina; }}

	[SerializeField] protected float _staminaMax = 10;
	public float staminaMax { get {return _staminaMax; }}

	[SerializeField] protected float _staminaRegen = 1;

	[Space(10)]
	public PlayerNumber player = PlayerNumber.Player1;
	public Camera playerCamera;

	private float lastShotTime;
    protected StatManager stat;
    protected float totalMoveTime;

	/*
	 * Living.Start() and stats init
	 */
    protected override void Start()
    {
		stat = StatManager.Instance;
		_maxLife += stat.Hp.Level;

        base.Start();
        totalMoveTime = 0;

		_staminaMax = _staminaMax + stat.End.Level;
        _stamina = _staminaMax;

		_jumpSpeed += stat.Spe.Level / 10f + stat.Rol.Level / 10f;
		_jumptime += stat.Rol.Level / 100f;

		lastShotTime = 1f;

		if (playerCamera == null) {
			playerCamera = Camera.main;
		}
    }

	/*
	 * Movement update
	 * Gun update
	 * Stats update
	 */
    protected override void Update() 
	{
		base.Update ();

		if (_controller.velocity == Vector3.zero && _audioSource.clip == stepSound)
			_audioSource.Pause ();

		// Update stat endurance if the stamina has been fully regenerated
		if (_stamina < _staminaMax && _stamina + _staminaRegen * Time.deltaTime > _staminaMax) {
			stat.End.Count++;
		}

		/*
		 * Stamina regen
		 */
		_stamina = Mathf.Clamp(_stamina + _staminaRegen * Time.deltaTime, 0, _staminaMax);

		/*
		 * Look at
		 */
		Vector3 LookAt = new Vector3 (MultiOSControls.GetValue ("RHorizontal", player), 0, -MultiOSControls.GetValue ("RVertical", player));
		if (LookAt == Vector3.zero && MultiOSControls.HasKeyboard(player)) {
			Vector3 cursorPos = Input.mousePosition;

			Vector3 playerScreenPos = playerCamera.WorldToScreenPoint (transform.position);

			LookAt = cursorPos - playerScreenPos;

			LookAt.z = LookAt.y;
			LookAt.y = 0;

			LookAt.Normalize ();
		}

		if (LookAt != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (LookAt);
		} else {
			LookAt = _moveDirection;

			LookAt.y = 0;

			LookAt.Normalize ();

			if (LookAt != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation (LookAt);
			}
		}


		/*
		 * Aim correction
		 */
		if (gun != null && MultiOSControls.GetValue ("Fire1", player) != 0) {
			RaycastHit hit;

			if (Physics.Raycast (transform.position, transform.forward, out hit, 10.0f, 8)) {// 8 = only living
				gun.transform.LookAt (transform.position + transform.forward * hit.distance);
			}
		}

		/*
		 * Movement
		 */
		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(MultiOSControls.GetValue ("Horizontal", player), 0, -MultiOSControls.GetValue ("Vertical", player));
			_moveDirection.Normalize ();

			if (MultiOSControls.GetValue ("Jump", player) != 0 && _stamina >= 2)
            {
				_stamina -= 2;
                StartCoroutine("Jump");

				// Update statmanager Rol stat
                stat.Rol.Count++;
            }
            else
            {
				// Update statmanager speed stat if player is moving
                if (_moveDirection.magnitude > 0.1)
                {
                    totalMoveTime += Time.deltaTime;
                    stat.Spe.Count = (int)totalMoveTime;
                }

				// Apply speed to normalized moveDirection (using the speed stat)
                _moveDirection *= _speed + (float)stat.Spe.Level / 10;
            }
		}

		if (_moveDirection.magnitude > 0.1 && !_audioSource.isPlaying && _moveDirection.y == 0) {
			_audioSource.clip = stepSound;
			_audioSource.loop = true;
			_audioSource.Play ();
		} else if (_moveDirection.magnitude < 0.1 && _audioSource.isPlaying) {
			_audioSource.Stop ();
		}

		/*
		 * Apply Movement
		 */
		_moveDirection.y -= _gravity * Time.deltaTime;
		_moveDirection *= Time.timeScale;
		_controller.Move(_moveDirection * Time.deltaTime);

		/*
		 * Gun managment
		 */
		if (_gun != null && MultiOSControls.GetValue ("Fire1", player) != 0) {
			lastShotTime = 0;
			_gun.Fire ();
		}
		
		if (_gun != null && MultiOSControls.GetValue ("Reload", player) != 0) {
			_gun.StartReload ();
		}

		lastShotTime += Time.deltaTime;
    }

	/*
	 * HACK force player away from enemy to avoid 'climbing him'
	 */
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag.Equals("Enemy") || hit.gameObject.tag.Equals("Boss"))
		{     
			Vector3 dir = hit.point - transform.position;
			dir = -dir.normalized;
			GetComponent<Rigidbody>().AddForce(dir * 200);
		}
		GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

	/*
	 * Jump coroutine handling airtime and rotation
	 */
    IEnumerator Jump()
    {

		_audioSource.clip = jumpSound;
		_audioSource.loop = false;
		_audioSource.Play ();

		float start = Time.realtimeSinceStartup;

		Vector3 planeModeDir = _moveDirection;
		planeModeDir.y = 0;

		planeModeDir.Normalize ();

		Vector3 rotationAxis = Quaternion.AngleAxis (90, Vector3.up) * planeModeDir;

		rotationAxis.Normalize ();

		_moveDirection *= _jumpSpeed;
        
		while (Time.realtimeSinceStartup - start < _jumptime)
        {
			_moveDirection.y = _heightCurve.Evaluate((Time.realtimeSinceStartup - start) / _jumptime);

			transform.RotateAround (transform.position, rotationAxis, _rollCurve.Evaluate((Time.realtimeSinceStartup - start) / _jumptime));
            yield return null;
        }
    }

	/*
	 * true if last shot is withing 0.5s
	 */
	public bool HasFired()
	{
		return lastShotTime < 0.5f;
	}

}
