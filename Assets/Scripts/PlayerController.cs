using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {
    
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    [Header("Player Stats")]
    [SerializeField] protected float _jumpSpeed = 6;
    [SerializeField] protected float _jumpHeight = 4;
    [Space(10)]
    public int levelAtk = 1;
    public int levelHp = 1;
    public int levelSpe = 1;
    public int levelEnd = 1;
    public int levelRan = 1;
    public int levelRol = 1;

    protected int expAtk = 0;
    protected int expHp = 0;
    protected int expSpe = 0;
    protected int expEnd = 0;
    protected int expRan = 0;
    protected int expRol = 0;

    protected int countAtk = 0;
    protected int countHp = 0;
    protected int countSpe = 0;
    protected int countEnd = 0;
    protected int countRan = 0;
    protected int countRol = 0;

    protected override void Start()
    {
        base.Start();
        stats.Add("Atk", levelAtk);
        stats.Add("Hp", levelHp);
        stats.Add("Spe", levelSpe);
        stats.Add("End", levelEnd);
        stats.Add("Ran", levelRan);
        stats.Add("Rol", levelRol);
    }

    protected override void Update() 
	{
		base.Update ();

		/*
		 * Look at
		 */
		if (_controller.isGrounded) {
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
		}

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
                countRol++;
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
            countAtk++;
		}
		
		if (_gun != null && Input.GetButtonDown ("Reload")) {
			_gun.StartReload ();
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

    protected void LevelUp(string stat)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat]++;
        }
        else
        {
            Debug.Log("erreur de statistique");
        }
    }
}
