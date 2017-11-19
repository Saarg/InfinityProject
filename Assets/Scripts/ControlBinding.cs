﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBinding : MonoBehaviour {
    public InputField UpField;
    public InputField DownField;
    public InputField LeftField;
    public InputField RightField;
    public InputField JumpField;
    public InputField FireField;

	public Dropdown Player1Controller;
	public Dropdown Player2Controller;
	public Dropdown Player3Controller;
	public Dropdown Player4Controller;

	public void ChooseController(Dropdown d, PlayerNumber p) {
		MultiOSControls.ChooseController (d.value, p);
	}

    // Checks if there is anything entered into the input field.
	public void BindUp(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("Up is" + input.text);
			MultiOSControls.BindNegKey ("Vertical", input.text);
            
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindDown(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("down is" + input.text);
			MultiOSControls.BindPosKey ("Vertical", input.text);
        }
        else
        {
        }
    }

    public void BindLeft(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("left is" + input.text);
			MultiOSControls.BindNegKey ("Horizontal", input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindRight(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("right is" + input.text);
			MultiOSControls.BindPosKey ("Horizontal", input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindJump(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("jump is" + input.text);
			MultiOSControls.BindPosKey ("Jump", input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindFire(InputField input)
    {
		if (ValidateInput(input.text))
        {
            Debug.Log("fire is" + input.text);
			MultiOSControls.BindPosKey ("Fire1", input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

	public bool ValidateInput(string input) {
		input = input.ToLower ();

		foreach (KeyCode n in System.Enum.GetValues(typeof(KeyCode))) {
			if (n.ToString ().ToLower () == input) {
				return true;
			}
		}

		return false;
	}

    public void Start()
    {
		Player1Controller.onValueChanged.AddListener(delegate { ChooseController(Player1Controller, PlayerNumber.Player1); });
		Player2Controller.onValueChanged.AddListener(delegate { ChooseController(Player2Controller, PlayerNumber.Player2); });
		Player3Controller.onValueChanged.AddListener(delegate { ChooseController(Player3Controller, PlayerNumber.Player3); });
		Player4Controller.onValueChanged.AddListener(delegate { ChooseController(Player4Controller, PlayerNumber.Player4); });

        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        UpField.onEndEdit.AddListener(delegate { BindUp(UpField); });
        DownField.onEndEdit.AddListener(delegate { BindDown(DownField); });
        LeftField.onEndEdit.AddListener(delegate { BindLeft(LeftField); });
        RightField.onEndEdit.AddListener(delegate { BindRight(RightField); });
        JumpField.onEndEdit.AddListener(delegate { BindJump(JumpField); });
        FireField.onEndEdit.AddListener(delegate { BindFire(FireField); });
    }
}
