using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script used to rebind controls from UI
 */
public class ControlBinding : MonoBehaviour {
    public InputField UpField;
    public InputField DownField;
    public InputField LeftField;
    public InputField RightField;
    public InputField JumpField;
	public InputField FireField;
    public InputField PauseField;

	public Dropdown Player1Controller;
	public Dropdown Player2Controller;
	public Dropdown Player3Controller;
	public Dropdown Player4Controller;

	public void ChooseController(Dropdown d, PlayerNumber p) {
		MultiOSControls.ChooseController (d.value, p);
	}

	public void BindPos(InputField input, string name)
	{
		if (ValidateInput(input.text))
		{
			MultiOSControls.BindPosKey (name, input.text);
		}
	}

	public void BindNeg(InputField input, string name)
	{
		if (ValidateInput(input.text))
		{
			MultiOSControls.BindNegKey (name, input.text);
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
		UpField.onEndEdit.AddListener(delegate { BindNeg(UpField, "Vertical"); });
		DownField.onEndEdit.AddListener(delegate { BindPos(DownField, "Vertical"); });
		LeftField.onEndEdit.AddListener(delegate { BindNeg(LeftField, "Horizontal"); });
		RightField.onEndEdit.AddListener(delegate { BindPos(RightField, "Horizontal"); });
		JumpField.onEndEdit.AddListener(delegate { BindPos(JumpField, "Jump"); });
		FireField.onEndEdit.AddListener(delegate { BindPos(FireField, "Fire1"); });
		PauseField.onEndEdit.AddListener(delegate { BindPos(PauseField, "Pause"); });

		if (File.Exists (Application.persistentDataPath + "/inputSettings.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/inputSettings.dat", FileMode.Open);
			ControlsSave save = (ControlsSave)bf.Deserialize (file);

			file.Close (); 

			UpField.text = save.up;
			BindNeg(UpField, "Vertical");
			DownField.text = save.down;
			BindPos(DownField, "Vertical");
			LeftField.text = save.left;
			BindNeg(LeftField, "Horizontal");
			RightField.text = save.right;
			BindPos(RightField, "Horizontal");
			JumpField.text = save.jump;
			BindPos(JumpField, "Jump");
			FireField.text = save.fire;
			BindPos(FireField, "Fire1");
			PauseField.text = save.pause;
			BindPos(PauseField, "Pause");

			Player1Controller.value = save.playerController1;
			Player2Controller.value = save.playerController2;
			Player3Controller.value = save.playerController3;
			Player4Controller.value = save.playerController4;
		}
    }

	void OnDestroy() {
		ControlsSave save = new ControlsSave ();

		save.up = UpField.text;
		save.down = DownField.text;
		save.left = LeftField.text;
		save.right = RightField.text;
		save.jump = JumpField.text;
		save.fire = FireField.text;
		save.pause = PauseField.text;

		save.playerController1 = Player1Controller.value;
		save.playerController2 = Player2Controller.value;
		save.playerController3 = Player3Controller.value;
		save.playerController4 = Player4Controller.value;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/inputSettings.dat");

		bf.Serialize(file, save);
		file.Close();
	}
}
/*
 * Serializable class used to save data into file
 */
[Serializable]
class ControlsSave
{
	public string up = "w";
	public string down = "s";
	public string left = "a";
	public string right = "d";
	public string jump = "space";
	public string fire = "mouse 0";
	public string pause = "p";

	public int playerController1 = 5;
	public int playerController2 = 0;
	public int playerController3 = 1;
	public int playerController4 = 2;
}