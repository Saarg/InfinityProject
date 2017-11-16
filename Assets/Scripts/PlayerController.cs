﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {

    //public Dictionary<string, PlayerStats> stats = new Dictionary<string, PlayerStats>();
    protected PlayerStats[] stats = new PlayerStats[6];

    [Header("Player Stats")]
    [SerializeField] protected float _jumpSpeed = 6;
	[SerializeField] protected float _jumptime = 1f;
	[SerializeField] protected AnimationCurve _heightCurve;
	[SerializeField] protected AnimationCurve _rollCurve;

    [Space(10)]
    public PlayerStats atk;
    public PlayerStats hp;
    public PlayerStats spe;
    public PlayerStats end;
    public PlayerStats ran;
    public PlayerStats rol;
    [Space(10)]
    public int[] experienceTable = new int[20];
    

    protected override void Start()
    {
        base.Start();
        

        atk = new PlayerStats();
        hp = new PlayerStats();
        spe = new PlayerStats();
        spe.ratio = 1000;
        end = new PlayerStats();
        ran = new PlayerStats();
        rol = new PlayerStats();

        stats[0] = atk;
        stats[1] = hp;
        stats[2] = spe;
        stats[3] = end;
        stats[4] = ran;
        stats[5] = rol;
    }

    protected override void Update() 
	{
		base.Update ();

		if (_controller.velocity == Vector3.zero && _audioSource.clip == stepSound)
			_audioSource.Pause ();

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
				print ("Found an object - distance: " + hit.distance);

				gun.transform.LookAt (transform.position + transform.forward * hit.distance);
			}
		}

		/*
		 * Movement
		 */
		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(MultiOSControls.GetValue ("Horizontal"), 0, -MultiOSControls.GetValue ("Vertical"));

			if (MultiOSControls.GetValue ("Jump") != 0)
            {
                StartCoroutine("Jump");
                rol.experience++;
                Debug.Log("Xp rol " + rol.experience);
            }
            else
            {
                _moveDirection *= _speed + (float)spe.level/10;
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
            spe.count++;
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
			_gun.Fire ();
            end.experience++;
            Debug.Log("Xp end " + end.experience);
		}
		
		if (_gun != null && MultiOSControls.GetValue ("Reload") != 0) {
			_gun.StartReload ();
		}

        if (Input.GetKeyDown("a"))
        {
            LevelUp();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        hp.count++;
    }

    IEnumerator Jump()
    {
		Debug.Log ("Coroutine de saut");

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

    protected void LevelUp()
    {

        foreach (PlayerStats ps in stats)
        {
            ps.Convert();
            while (ps.LevelUp(experienceTable[ps.level]))
            {
                Debug.Log("Level :" + ps.level +" xp restant :"+ ps.experience);
            }
        }
    }
}
