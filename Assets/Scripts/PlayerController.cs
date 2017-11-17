using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {
    
    protected PlayerStats[] stats = new PlayerStats[6];

    [Header("Player Stats")]
    [SerializeField] protected float _jumpSpeed = 6;
	[SerializeField] protected float _jumptime = 1f;
	[SerializeField] protected AnimationCurve _heightCurve;
	[SerializeField] protected AnimationCurve _rollCurve;

    protected StatManager stat;
    
	protected float _stamina = 10;
	public float stamina { get {return _stamina; }}
	[SerializeField] protected float _staminaMax = 10;
	public float staminaMax { get {return _staminaMax; }}
	[SerializeField] protected float _staminaRegen = 1;

	private float lastShotTime;

    protected override void Start()
    {
        base.Start();
        stat = StatManager.Instance;

		_stamina = _staminaMax;
    }

    protected override void Update() 
	{
		base.Update ();

		if (_controller.velocity == Vector3.zero && _audioSource.clip == stepSound)
			_audioSource.Pause ();

		/*
		 * Stamina regen
		 */
		_stamina = Mathf.Clamp(_stamina + _staminaRegen * Time.deltaTime, 0, _staminaMax);

		/*
		 * Look at
		 */
		Vector3 LookAt = new Vector3 (MultiOSControls.GetValue ("RHorizontal"), 0, -MultiOSControls.GetValue ("RVertical"));
		if (LookAt == Vector3.zero) {
			Vector3 cursorPos = Input.mousePosition;

			Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (transform.position);

			LookAt = cursorPos - playerScreenPos;

			LookAt.z = LookAt.y;
			LookAt.y = 0;

			LookAt.Normalize ();
		}

		transform.rotation = Quaternion.LookRotation (LookAt);

		/*
		 * Aim correction
		 */
		if (gun != null && MultiOSControls.GetValue ("Fire1") != 0) {
			RaycastHit hit;

			if (Physics.Raycast (transform.position, transform.forward, out hit, 10.0f)) {
				// print ("Found an object - distance: " + hit.distance);

				gun.transform.LookAt (transform.position + transform.forward * hit.distance);
			}
		}

		/*
		 * Movement
		 */
		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(MultiOSControls.GetValue ("Horizontal"), 0, -MultiOSControls.GetValue ("Vertical"));

			if (MultiOSControls.GetValue ("Jump") != 0 && _stamina >= 2)
            {
				_stamina -= 2;
                StartCoroutine("Jump");
                stat.Rol.Experience++;
            }
            else
            {
                _moveDirection *= _speed + (float)stat.Spe.Level/10;
            }
		}
        else
        {
            
        }

		if (_moveDirection.magnitude > 0.1 && !_audioSource.isPlaying && _moveDirection.y == 0) {
			_audioSource.clip = stepSound;
			_audioSource.loop = true;
			_audioSource.Play ();
		} else if (_moveDirection.magnitude < 0.1 && _audioSource.isPlaying) {
			_audioSource.Stop ();
		}

        if(_moveDirection.magnitude > 0.1)
        {
            stat.Spe.Count++;
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
		if (_gun != null && MultiOSControls.GetValue ("Fire1") != 0) {
			lastShotTime = 0;
			_gun.Fire ();
            stat.End.Count++;
		}
		
		if (_gun != null && MultiOSControls.GetValue ("Reload") != 0) {
			_gun.StartReload ();
		}

		lastShotTime += Time.deltaTime;

        if (Input.GetKeyDown("a"))
        {
            stat.LevelUp();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        stat.Hp.Count++;
    }

    IEnumerator Jump()
    {

		_audioSource.clip = jumpSound;
		_audioSource.loop = false;
		_audioSource.Play ();

		float start = Time.realtimeSinceStartup;

		Vector3 planeModeDir = _moveDirection;
		planeModeDir.y = 0;

		Vector3 rotationAxis = Quaternion.AngleAxis (90, Vector3.up) * planeModeDir;

		_moveDirection *= _jumpSpeed;
        
		while (Time.realtimeSinceStartup - start < _jumptime)
        {
			_moveDirection.y = _heightCurve.Evaluate((Time.realtimeSinceStartup - start) / _jumptime);

			transform.RotateAround (transform.position, rotationAxis, _rollCurve.Evaluate((Time.realtimeSinceStartup - start) / _jumptime));
            yield return null;
        }
    }

	public bool HasFired()
	{
		if (lastShotTime < 0.5f)
			return true;

		return false;
	}

}
