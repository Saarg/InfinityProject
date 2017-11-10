using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {

    //public Dictionary<string, PlayerStats> stats = new Dictionary<string, PlayerStats>();
    protected PlayerStats[] stats = new PlayerStats[6];

    [Header("Player Stats")]
    [SerializeField] protected float _jumpSpeed = 6;
    [SerializeField] protected float _jumpHeight = 4;
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

		/*
		 * Look at
		 */
//		if (_controller.isGrounded) {
			Vector3 LookAt = new Vector3 (Input.GetAxisRaw ("RHorizontal"), 0, -Input.GetAxisRaw ("RVertical"));
			if (LookAt != Vector3.zero)
				transform.rotation = Quaternion.LookRotation (LookAt);
			else { // If no controler use mouse
				Vector3 cursorPos = Input.mousePosition;

				Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (transform.position);

				Vector3 PtoC = cursorPos - playerScreenPos;
				PtoC.Normalize ();

				PtoC.z = PtoC.y;
				PtoC.y = 0;

				transform.rotation = Quaternion.LookRotation (PtoC);

				Debug.DrawLine (transform.position, transform.position + PtoC, Color.grey);
			}
//		}

		/*
		 * Movement
		 */
		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

			if (Input.GetButtonDown ("Jump"))
            {
                _moveDirection *= _jumpSpeed;
                _moveDirection.y = _jumpHeight;
                StartCoroutine("Jump");
                rol.experience++;
                Debug.Log("Xp rol " + rol.experience);
            }
            else
            {
                _moveDirection *= _speed;
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

		/*
		 * Apply Movement
		 */
		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);

		/*
		 * Gun managment
		 */
		if (_gun != null && Input.GetButtonDown ("Fire1")) {
			_gun.Fire ();
            atk.experience++;
            Debug.Log("Xp atk " + atk.experience);
		}
		
		if (_gun != null && Input.GetButtonDown ("Reload")) {
			_gun.StartReload ();
		}

        if (Input.GetKeyDown("a"))
        {
            LevelUp();
        }
    }

    IEnumerator Jump()
    {
		Debug.Log ("Coroutine de saut");

		_audioSource.clip = jumpSound;
		_audioSource.loop = false;
		_audioSource.Play ();

		Vector3 planeModeDir = _moveDirection;
		planeModeDir.y = 0;

		Vector3 rotationAxis = Quaternion.AngleAxis (90, Vector3.up) * planeModeDir;
        
        for (int i = 0; i < 18; i++)
        {
			transform.RotateAround (transform.position, rotationAxis, 20);
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    protected void LevelUp()
    {
        foreach(PlayerStats ps in stats)
        {
            while(ps.LevelUp(experienceTable[ps.level]))
            {
                Debug.Log("Level :" + ps.level +" xp restant :"+ ps.experience);
            }
        }
    }
}
